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
                    if (instance is null) Init();
                }
            }
            return instance;
        }
    }
    private static void Init()
    {
        instance = new Cat5BotDB();
        dbLock = new object();
        db = new DB();
    }
    #endregion Singleton

    private static object dbLock;
    private static DB db;
    
    public static void Insert(DBEntry dbEntry)
    {

    }

    public static void Remove(DBEntry dbEntry)
    {
        
    }

    public static DBEntry Query(QueryDBAction dbAction)
    {
        lock (dbLock)
        {
            return db.Query(dbAction);
        }
    }
}