using System.Collections.Concurrent;
using System.Threading;

namespace Cat5Bot.DB;

public sealed class Cat5BotDB
{
    #region Singleton
    private Cat5BotDB() {}
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
                        instance = new Cat5BotDB();
                        I.dbLock = new object();
                        I.db = new DB();
                    }
                }
            }
            return instance;
        }
    }
    #endregion Singleton

    #region BasicFunctionality
    private object dbLock;
    private DB db;

    public static void WriteAll()
    {
        lock (I.dbLock) I.db.WriteAll();
    }
    public static void ReadAll()
    {
        lock (I.dbLock) I.db.ReadAll();
    }

    public static void Insert(AliasedStringDBEntry entry)
    {
        lock (I.dbLock) I.db.Insert(entry);
    }
    public static void Insert(EventDBEntry entry)
    {
        lock (I.dbLock) I.db.Insert(entry);
    }
    public static void Insert(AttendanceDBEntry entry)
    {
        lock (I.dbLock) I.db.Insert(entry);
    }

    public static bool Remove(Predicate<AliasedStringDBEntry> remove)
    {
        lock (I.dbLock) return I.db.Remove(remove);
    }
    public static bool Remove(Predicate<EventDBEntry> remove)
    {
        lock (I.dbLock) return I.db.Remove(remove);
    }
    public static bool Remove(Predicate<AttendanceDBEntry> remove)
    {
        lock (I.dbLock) return I.db.Remove(remove);
    }

    public static bool Query(Predicate<AliasedStringDBEntry> query, out List<AliasedStringDBEntry> entries)
    {
        lock (I.dbLock) return I.db.Query(query, out entries);
    }
    public static bool Query(Predicate<EventDBEntry> query, out List<EventDBEntry> entries)
    {
        lock (I.dbLock) return I.db.Query(query, out entries);
    }
    public static bool Query(Predicate<AttendanceDBEntry> query, out List<AttendanceDBEntry> entries)
    {
        lock (I.dbLock) return I.db.Query(query, out entries);
    }
    public static bool Query(Predicate<AliasedStringDBEntry> query, out AliasedStringDBEntry entry)
    {
        lock (I.dbLock) return I.db.Query(query, out entry);
    }
    public static bool Query(Predicate<EventDBEntry> query, out EventDBEntry entry)
    {
        lock (I.dbLock) return I.db.Query(query, out entry);
    }
    public static bool Query(Predicate<AttendanceDBEntry> query, out AttendanceDBEntry entry)
    {
        lock (I.dbLock) return I.db.Query(query, out entry);
    }
    #endregion BasicFunctionality

    #region ExtendedFunctionality
    public static void LinkAttendee(ulong attendee, string name)
    {
        if (Query((e) => e.alias == attendee, out AliasedStringDBEntry e))
        {
            e.str = name;
        }
        else
        {
            Insert(new AliasedStringDBEntry(attendee, name));
        }
    }
    #endregion ExtendedFunctionality
}