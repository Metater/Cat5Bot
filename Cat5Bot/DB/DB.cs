using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public class DB
{
    private string nameAliasesDBPath => Directory.GetCurrentDirectory() + @"\nameAliases.db";
    private string eventsDBPath => Directory.GetCurrentDirectory() + @"\events.db";
    private string attendanceDBPath => Directory.GetCurrentDirectory() + @"\attendance.db";

    private readonly List<AliasedStringDBEntry> nameAliases = new();
    private readonly List<EventDBEntry> events = new();
    private readonly List<AttendanceDBEntry> attendance = new();

    public DB()
    {
        ReadAll().GetAwaiter().GetResult();
    }

    public async Task WriteAll()
    {
        DBWriter dbWriter = new();
        foreach (AliasedStringDBEntry nameAlias in nameAliases)
            nameAlias.Serialize(dbWriter);
        await File.WriteAllBytesAsync(nameAliasesDBPath, dbWriter.CopyData());
        dbWriter.Reset();
        foreach (EventDBEntry @event in events)
            @event.Serialize(dbWriter);
        await File.WriteAllBytesAsync(eventsDBPath, dbWriter.CopyData());
        dbWriter.Reset();
        foreach (AttendanceDBEntry attendanceRecord in attendance)
            attendanceRecord.Serialize(dbWriter);
        await File.WriteAllBytesAsync(attendanceDBPath, dbWriter.CopyData());
    }

    public async Task ReadAll()
    {
        nameAliases.Clear();
        events.Clear();
        attendance.Clear();
        DBReader dbReader = new();
        if (File.Exists(nameAliasesDBPath))
        {
            dbReader.SetSource(await File.ReadAllBytesAsync(nameAliasesDBPath));
            while (!dbReader.EndOfData)
                nameAliases.Add(new AliasedStringDBEntry(dbReader));
        }
        if (File.Exists(eventsDBPath))
        {
            dbReader.SetSource(await File.ReadAllBytesAsync(eventsDBPath));
            while (!dbReader.EndOfData)
                events.Add(new EventDBEntry(dbReader));
        }
        if (File.Exists(attendanceDBPath))
        {
            dbReader.SetSource(await File.ReadAllBytesAsync(attendanceDBPath));
            while (!dbReader.EndOfData)
                attendance.Add(new AttendanceDBEntry(dbReader));
        }
    }

    public void InsertNameAlias(ulong alias, string name, out AliasedStringDBEntry entry)
    {
        entry = new AliasedStringDBEntry(alias, name);
        nameAliases.Add(entry);
    }

    public bool QueryNameAlias(ulong alias, out AliasedStringDBEntry entry)
    {
        entry = nameAliases.Find((e) => e.alias == alias);
        return entry is not null;
    }

    public bool InsertEvent(string name, string eventType, DateTime time, TimeSpan length, out EventDBEntry entry)
    {
        ulong eventId = (ulong)DateTime.UtcNow.ToFileTimeUtc();
        entry = new EventDBEntry(eventId, name, eventType, time, length);
        events.Add(eventId, entry);
        return true;
    }

    public bool QueryEvent(ulong eventId, out EventDBEntry entry)
    {
        return events.TryGetValue(eventId, out entry);
    }

    public bool InsertAttendance(ulong attendee, out AttendanceDBEntry entry)
    {
        entry = new AttendanceDBEntry(attendee);
        if (!attendance.TryAdd(attendee, entry))
            return QueryAttendance(attendee, out entry);
        return true;
    }

    public bool QueryAttendance(ulong attendee, out AttendanceDBEntry entry)
    {
        return attendance.TryGetValue(attendee, out entry);
    }
}

public enum DBType
{
    NameAliases,
    Events,
    Attendance
}