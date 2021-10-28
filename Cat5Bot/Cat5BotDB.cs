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

    private ConcurrentQueue dbActionQueue = new ConcurrentQueue();

    public void Tick()
    {
        
    }
}