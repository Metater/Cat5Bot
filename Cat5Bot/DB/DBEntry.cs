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
        dbWriter.Put((byte)type);
        dbWriter.Put(alias);
        dbWriter.Put(str);
    }

    public override void Deserialize(DBReader dbReader)
    {
        alias = dbReader.GetULong();
        str = dbReader.GetString();
    }
}

public class AttendanceDBEntry : DBEntry
{
    public ulong attendee;
    public List<ulong> attendedEvents;

    public AttendanceDBEntry(ulong attendee)
    {
        type = DBEntryType.Attendance;
        this.attendee = attendee;
        attendedEvents = new List<ulong>();
    }

    public AttendanceDBEntry(DBReader dbReader)
    {
        type = DBEntryType.Attendance;
        Deserialize(dbReader);
    }

    public bool Attend(ulong attendedEvent)
    {
        if (attendedEvents.Contains(attendedEvent)) return false;
        attendedEvents.Add(attendedEvent);
        return true;
    }

    public bool Unattend(ulong attendedEvent)
    {
        if (attendedEvents.Contains(attendedEvent)) return false;
        attendedEvents.Remove(attendedEvent);
        return true;
    }

    public override void Serialize(DBWriter dbWriter)
    {
        dbWriter.Put((byte)type);
        dbWriter.Put(attendee);
        dbWriter.PutArray(attendedEvents.ToArray());
    }

    public override void Deserialize(DBReader dbReader)
    {
        attendee = dbReader.GetULong();
        attendedEvents = new List<ulong>(dbReader.GetULongArray());
    }
}


public enum DBEntryType : byte
{
    AliasedString,
    Attendance
}