using System.Collections.Concurrent;
using System.Threading;

namespace Cat5Bot.DB;

public sealed class Cat5BotDB
{
    #region Singleton
    private Cat5BotDB()
    {
        dbLock = new();
        db = new();
    }
    private static Cat5BotDB instance = null;
    private static readonly object instanceLock = new();
    public static Cat5BotDB I
    {
        get
        {
            if (instance is null)
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new();
                    }
                }
            }
            return instance;
        }
    }
    #endregion Singleton

    #region BasicFunctionality
    private readonly object dbLock;
    private readonly DB db;

    public void WriteAll()
    {
        lock (dbLock) db.WriteAll();
    }
    public void ReadAll()
    {
        lock (dbLock) db.ReadAll();
    }

    public void Insert(AliasedStringDBEntry entry)
    {
        lock (dbLock) db.Insert(entry);
    }
    public void Insert(AliasedByteDBEntry entry)
    {
        lock (dbLock) db.Insert(entry);
    }
    public void Insert(EventDBEntry entry)
    {
        lock (dbLock) db.Insert(entry);
    }
    public void Insert(AttendanceDBEntry entry)
    {
        lock (dbLock) db.Insert(entry);
    }

    public bool Remove(Predicate<AliasedStringDBEntry> remove)
    {
        lock (dbLock) return db.Remove(remove);
    }
    public bool Remove(Predicate<AliasedByteDBEntry> remove)
    {
        lock (dbLock) return db.Remove(remove);
    }
    public bool Remove(Predicate<EventDBEntry> remove)
    {
        lock (dbLock) return db.Remove(remove);
    }
    public bool Remove(Predicate<AttendanceDBEntry> remove)
    {
        lock (dbLock) return db.Remove(remove);
    }

    public bool Query(Predicate<AliasedStringDBEntry> query, out List<AliasedStringDBEntry> entries)
    {
        lock (dbLock) return db.Query(query, out entries);
    }
    public bool Query(Predicate<AliasedByteDBEntry> query, out List<AliasedByteDBEntry> entries)
    {
        lock (dbLock) return db.Query(query, out entries);
    }
    public bool Query(Predicate<EventDBEntry> query, out List<EventDBEntry> entries)
    {
        lock (dbLock) return db.Query(query, out entries);
    }
    public bool Query(Predicate<AttendanceDBEntry> query, out List<AttendanceDBEntry> entries)
    {
        lock (dbLock) return db.Query(query, out entries);
    }
    public bool Query(Predicate<AliasedStringDBEntry> query, out AliasedStringDBEntry entry)
    {
        lock (dbLock) return db.Query(query, out entry);
    }
    public bool Query(Predicate<AliasedByteDBEntry> query, out AliasedByteDBEntry entry)
    {
        lock (dbLock) return db.Query(query, out entry);
    }
    public bool Query(Predicate<EventDBEntry> query, out EventDBEntry entry)
    {
        lock (dbLock) return db.Query(query, out entry);
    }
    public bool Query(Predicate<AttendanceDBEntry> query, out AttendanceDBEntry entry)
    {
        lock (dbLock) return db.Query(query, out entry);
    }
    #endregion BasicFunctionality

    #region ExtendedFunctionality
    public static void LinkAttendee(ulong attendee, string name)
    {
        if (I.Query((e) => e.alias == attendee, out AliasedStringDBEntry e))
        {
            e.str = name;
        }
        else
        {
            I.Insert(new AliasedStringDBEntry(attendee, name));
        }
    }
    public static void UpdatePermission(ulong member, byte permission)
    {
        if (I.Query((e) => e.alias == member, out AliasedByteDBEntry e))
        {
            e.bite = permission;
        }
        else
        {
            I.Insert(new AliasedByteDBEntry(member, permission));
        }
    }
    #endregion ExtendedFunctionality
}