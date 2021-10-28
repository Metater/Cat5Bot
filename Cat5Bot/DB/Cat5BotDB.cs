using System.Collections.Concurrent;

public sealed class Cat5BotDB
{
    #region Singleton
    private Cat5BotDB() {}
    private static Cat5BotDB instance = null;
    private static readonly object instanceLock = new object();
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

    private ConcurrentQueue<DBAction> dbActionQueue = new ConcurrentQueue<DBAction>();
    
    public void Insert(DBEntry dbEntry)
    {

    }

    public static async Task<DBEntry> Query(DBAction dbAction)
    {
        TaskCompletionSource<DBEntry> query = new TaskCompletionSource<DBEntry>();
        dbAction.Completed += (entry) =>
        {
            query.SetResult(entry);
        }
        dbActionQueue.Enqueue(dbAction);
        return query.Task;
    }

    public void Execute()
    {
        // ensure loaded in memory
        int dbActionCount = dbActionQueue.Count;
        for (int i = 0; i < dbActionCount; i++)
        {
            if (dbActionQueue.TryDequeue(out DBAction dbAction))
                ProcessDBAction(dbAction);
            else
                break;
        }
    }

    private void ProcessDBAction(DBAction dbAction)
    {
        switch (dbAction.type)
        {
            case DBActionType.Query:
                ((QueryDBAction)dbAction).Completed?.Invoke(new DBEntry());
                break;
        }
    }
}