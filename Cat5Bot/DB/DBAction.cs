using System;

public abstract class DBAction
{
    public DBActionType type;
}

public class QueryDBAction
{
    public Action<DBEntry> Completed;

    public QueryDBAction()
    {
        type = DBActionType.Query;
    }
}


public enum DBActionType
{
    Insert,
    Delete,
    Query,
}