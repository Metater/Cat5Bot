using System;

namespace Cat5Bot.DB; //{}

public abstract class DBEntry : IDBSerializable
{
    public DBEntryType Type { get; protected set; }
    public abstract void Serialize(DBWriter dbWriter);
    public abstract void Deserialize(DBReader dbReader);
}

public class AliasedStringDBEntry : DBEntry
{
    public ulong Alias { get; private set; }
    public string str;

    public AliasedStringDBEntry(ulong alias, string str)
    {
        Type = DBEntryType.AliasedString;
        Alias = alias;
        this.str = str;
    }

    public AliasedStringDBEntry(DBReader dbReader)
    {
        Type = DBEntryType.AliasedString;
        Deserialize(dbReader);
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(Alias);
        dbWriter.Put(str);
    }

    public override void Deserialize(DBReader dbReader)
    {
        Alias = dbReader.GetULong();
        str = dbReader.GetString();
    }
}

public class AliasedByteDBEntry : DBEntry
{
    public ulong Alias { get; private set; }
    public byte bite;

    public AliasedByteDBEntry(ulong alias, byte bite)
    {
        Type = DBEntryType.AliasedByte;
        Alias = alias;
        this.bite = bite;
    }

    public AliasedByteDBEntry(DBReader dbReader)
    {
        Type = DBEntryType.AliasedByte;
        Deserialize(dbReader);
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(Alias);
        dbWriter.Put(bite);
    }

    public override void Deserialize(DBReader dbReader)
    {
        Alias = dbReader.GetULong();
        bite = dbReader.GetByte();
    }
}

public class EventDBEntry : DBEntry
{
    public ulong EventId { get; private set; }
    public string Name { get; private set; }
    public string eventType;
    public DateTime time;
    public TimeSpan length;

    public EventDBEntry(ulong eventId, string name, string eventType, DateTime time, TimeSpan length)
    {
        Type = DBEntryType.Event;
        EventId = eventId;
        Name = name;
        this.eventType = eventType;
        this.time = time;
        this.length = length;
    }

    public EventDBEntry(DBReader dbReader)
    {
        Type = DBEntryType.Event;
        Deserialize(dbReader);
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(EventId);
        dbWriter.Put(Name);
        dbWriter.Put(eventType);
        dbWriter.Put(time.ToFileTime());
        dbWriter.Put(length.TotalSeconds);
    }

    public override void Deserialize(DBReader dbReader)
    {
        EventId = dbReader.GetULong();
        Name = dbReader.GetString();
        eventType = dbReader.GetString();
        time = DateTime.FromFileTime(dbReader.GetLong());
        length = TimeSpan.FromSeconds(dbReader.GetDouble());
    }
}

public class AttendanceDBEntry : DBEntry
{
    public ulong Attendee { get; private set; }
    public List<ulong> EventIds { get; private set; }

    public AttendanceDBEntry(ulong attendee)
    {
        Type = DBEntryType.Attendance;
        Attendee = attendee;
        EventIds = new List<ulong>();
    }

    public AttendanceDBEntry(DBReader dbReader)
    {
        Type = DBEntryType.Attendance;
        Deserialize(dbReader);
    }

    public bool Attend(ulong eventId)
    {
        if (EventIds.Contains(eventId)) return false;
        EventIds.Add(eventId);
        return true;
    }

    public bool Unattend(ulong eventId)
    {
        if (!EventIds.Contains(eventId)) return false;
        EventIds.Remove(eventId);
        return true;
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put(Attendee);
        dbWriter.PutArrayLong(EventIds.ToArray());
    }

    public override void Deserialize(DBReader dbReader)
    {
        Attendee = dbReader.GetULong();
        EventIds = new List<ulong>(dbReader.GetULongArrayLong());
    }
}


public enum DBEntryType : byte
{
    AliasedString,
    AliasedByte,
    Event,
    Attendance
}