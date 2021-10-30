using System;

namespace Cat5Bot.DB;

public abstract class DBEntry : IDBSerializable
{
    public DBEntryType type;
    public abstract void Serialize(DBWriter dbWriter);
    public abstract void Deserialize(DBReader dbReader);
}

public class AliasedStringDBEntry : DBEntry
{
    public ulong alias;
    public string str;
    
    public AliasedStringDBEntry(ulong alias, string str)
    {
        type = DBEntryType.AliasedString;
        this.alias = alias;
        this.str = str;
    }

    public AliasedStringDBEntry(DBReader dbReader)
    {
        type = DBEntryType.AliasedString;
        Deserialize(dbReader);
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(alias);
        dbWriter.Put(str);
    }

    public override void Deserialize(DBReader dbReader)
    {
        alias = dbReader.GetULong();
        str = dbReader.GetString();
    }
}

public class EventDBEntry : DBEntry
{
    public ulong eventId;
    public string name;
    public string eventType;
    public DateTime time;
    public TimeSpan length;

    public EventDBEntry(ulong eventId, string name, string eventType, DateTime time, TimeSpan length)
    {
        type = DBEntryType.Event;
        this.eventId = eventId;
        this.name = name;
        this.eventType = eventType;
        this.time = time;
        this.length = length;
    }

    public EventDBEntry(DBReader dbReader)
    {
        type = DBEntryType.Event;
        Deserialize(dbReader);
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(eventId);
        dbWriter.Put(name);
        dbWriter.Put(eventType);
        dbWriter.Put(time.ToFileTime());
        dbWriter.Put(length.TotalSeconds);
    }

    public override void Deserialize(DBReader dbReader)
    {
        eventId = dbReader.GetULong();
        name = dbReader.GetString();
        eventType = dbReader.GetString();
        time = DateTime.FromFileTime(dbReader.GetLong());
        length = TimeSpan.FromSeconds(dbReader.GetDouble());
    }
}

public class AttendanceDBEntry : DBEntry
{
    public ulong attendee;
    public List<ulong> eventIds;

    public AttendanceDBEntry(ulong attendee)
    {
        type = DBEntryType.Attendance;
        this.attendee = attendee;
        eventIds = new List<ulong>();
    }

    public AttendanceDBEntry(DBReader dbReader)
    {
        type = DBEntryType.Attendance;
        Deserialize(dbReader);
    }

    public bool Attend(ulong eventId)
    {
        if (eventIds.Contains(eventId)) return false;
        eventIds.Add(eventId);
        return true;
    }

    public bool Unattend(ulong eventId)
    {
        if (eventIds.Contains(eventId)) return false;
        eventIds.Remove(eventId);
        return true;
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(attendee);
        dbWriter.PutArrayLong(eventIds.ToArray());
    }

    public override void Deserialize(DBReader dbReader)
    {
        attendee = dbReader.GetULong();
        eventIds = new List<ulong>(dbReader.GetULongArrayLong());
    }
}


public enum DBEntryType : byte
{
    AliasedString,
    Event,
    Attendance
}