namespace Cat5Bot.DB; //{}

public class DB
{
    private string nameDBPath => Directory.GetCurrentDirectory() + @"\names.db";
    private string permissionsDBPath => Directory.GetCurrentDirectory() + @"\permissions.db";
    private string eventsDBPath => Directory.GetCurrentDirectory() + @"\events.db";
    private string attendanceDBPath => Directory.GetCurrentDirectory() + @"\attendance.db";

    private readonly List<AliasedStringDBEntry> nameAliases = new();
    private readonly List<AliasedByteDBEntry> permissions = new();
    private readonly List<EventDBEntry> events = new();
    private readonly List<AttendanceDBEntry> attendanceRecords = new();

    public DB()
    {
        ReadAll();
    }

    public void WriteAll()
    {
        DBWriter dbWriter = new();

        foreach (AliasedStringDBEntry nameAlias in nameAliases)
            nameAlias.Serialize(dbWriter);
        File.WriteAllBytes(nameDBPath, dbWriter.CopyData());
        dbWriter.Reset();

        foreach (AliasedByteDBEntry permission in permissions)
            permission.Serialize(dbWriter);
        File.WriteAllBytes(permissionsDBPath, dbWriter.CopyData());
        dbWriter.Reset();

        foreach (EventDBEntry @event in events)
            @event.Serialize(dbWriter);
        File.WriteAllBytes(eventsDBPath, dbWriter.CopyData());
        dbWriter.Reset();

        foreach (AttendanceDBEntry attendanceRecord in attendanceRecords)
            attendanceRecord.Serialize(dbWriter);
        File.WriteAllBytes(attendanceDBPath, dbWriter.CopyData());
    }

    public void ReadAll()
    {
        nameAliases.Clear();
        permissions.Clear();
        events.Clear();
        attendanceRecords.Clear();
        DBReader dbReader = new();
        if (File.Exists(nameDBPath))
        {
            dbReader.SetSource(File.ReadAllBytes(nameDBPath));
            while (!dbReader.EndOfData)
                nameAliases.Add(new AliasedStringDBEntry(dbReader));
        }
        if (File.Exists(permissionsDBPath))
        {
            dbReader.SetSource(File.ReadAllBytes(permissionsDBPath));
            while (!dbReader.EndOfData)
                permissions.Add(new AliasedByteDBEntry(dbReader));
        }
        if (File.Exists(eventsDBPath))
        {
            dbReader.SetSource(File.ReadAllBytes(eventsDBPath));
            while (!dbReader.EndOfData)
                events.Add(new EventDBEntry(dbReader));
        }
        if (File.Exists(attendanceDBPath))
        {
            dbReader.SetSource(File.ReadAllBytes(attendanceDBPath));
            while (!dbReader.EndOfData)
                attendanceRecords.Add(new AttendanceDBEntry(dbReader));
        }
    }

    public void Insert(AliasedStringDBEntry entry)
    {
        nameAliases.Add(entry);
    }
    public void Insert(AliasedByteDBEntry entry)
    {
        permissions.Add(entry);
    }
    public void Insert(EventDBEntry entry)
    {
        events.Add(entry);
    }
    public void Insert(AttendanceDBEntry entry)
    {
        attendanceRecords.Add(entry);
    }

    public bool Remove(Predicate<AliasedStringDBEntry> remove)
    {
        return 0 != nameAliases.RemoveAll(remove);
    }
    public bool Remove(Predicate<AliasedByteDBEntry> remove)
    {
        return 0 != permissions.RemoveAll(remove);
    }
    public bool Remove(Predicate<EventDBEntry> remove)
    {
        return 0 != events.RemoveAll(remove);
    }
    public bool Remove(Predicate<AttendanceDBEntry> remove)
    {
        return 0 != attendanceRecords.RemoveAll(remove);
    }

    public bool Query(Predicate<AliasedStringDBEntry> query, out List<AliasedStringDBEntry> entries)
    {
        entries = nameAliases.FindAll(query);
        return entries.Count > 0;
    }
    public bool Query(Predicate<AliasedByteDBEntry> query, out List<AliasedByteDBEntry> entries)
    {
        entries = permissions.FindAll(query);
        return entries.Count > 0;
    }
    public bool Query(Predicate<EventDBEntry> query, out List<EventDBEntry> entries)
    {
        entries = events.FindAll(query);
        return entries.Count > 0;
    }
    public bool Query(Predicate<AttendanceDBEntry> query, out List<AttendanceDBEntry> entries)
    {
        entries = attendanceRecords.FindAll(query);
        return entries.Count > 0;
    }
    public bool Query(Predicate<AliasedStringDBEntry> query, out AliasedStringDBEntry entry)
    {
        entry = nameAliases.Find(query);
        return entry is not null;
    }
    public bool Query(Predicate<AliasedByteDBEntry> query, out AliasedByteDBEntry entry)
    {
        entry = permissions.Find(query);
        return entry is not null;
    }
    public bool Query(Predicate<EventDBEntry> query, out EventDBEntry entry)
    {
        entry = events.Find(query);
        return entry is not null;
    }
    public bool Query(Predicate<AttendanceDBEntry> query, out AttendanceDBEntry entry)
    {
        entry = attendanceRecords.Find(query);
        return entry is not null;
    }
}