using System.Collections.Concurrent;

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
                    if (instance is null) instance = new Cat5BotDB();
                }
            }
            return instance;
        }
    }
    #endregion Singleton

    private readonly ConcurrentQueue<DBAction> dbActionQueue = new();
    
    public static void Insert(DBEntry dbEntry)
    {

    }

    public static async Task<EntryDBActionResult> Query(QueryDBAction dbAction)
    {
        TaskCompletionSource<EntryDBActionResult> query = new();
        dbAction.Completed += (entry) =>
        {
            query.SetResult((EntryDBActionResult)entry);
        };
        I.dbActionQueue.Enqueue(dbAction);
        return await query.Task;
    }

    public static void HandleQueuedActions()
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
        DBActionResult actionResult;
        switch (dbAction.type)
        {
            case DBActionType.Query:
                actionResult = new EntryDBActionResult();
                break;
        }
        dbAction.Completed?.Invoke(actionResult);
    }
}