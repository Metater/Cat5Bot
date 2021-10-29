using System;

namespace Cat5Bot.DB;

public abstract class DBAction
{
    public DBActionType type;
    public Action<DBActionResult> Completed;
}

public class QueryDBAction : DBAction
{
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