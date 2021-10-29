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
    private void Init()
    {
        instance = new Cat5BotDB();
        dbLock = new object();
        db = new DB();
    }
    #endregion Singleton

    private object dbLock;
    private DB db;
    
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

    public static async Task<QueryDBActionResult> Query(QueryDBAction dbAction)
    {
        //https://docs.microsoft.com/en-us/dotnet/standard/threading/how-to-use-spinlock-for-low-level-synchronization
        TaskCompletionSource<QueryDBActionResult> query = new();
        dbAction.Completed += (entry) =>
        {
            query.SetResult(entry.Query());
        };
        I.dbActionQueue.Enqueue(dbAction);
        return await query.Task;
    }

    public static void ProcessQueuedDBActions()
    {
        // ensure loaded in memory
        int dbActionCount = I.dbActionQueue.Count;
        for (int i = 0; i < dbActionCount; i++)
        {
            if (I.dbActionQueue.TryDequeue(out DBAction dbAction))
                ProcessDBAction(dbAction);
            else
                break;
        }
    }

    private static void ProcessDBAction(DBAction dbAction)
    {
        DBActionResult actionResult = null;
        switch (dbAction.type)
        {
            case DBActionType.Query:
                actionResult = new QueryDBActionResult();
                break;
        }
        dbAction.Completed?.Invoke(actionResult);
    }
}