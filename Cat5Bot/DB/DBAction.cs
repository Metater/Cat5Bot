using System;

namespace Cat5Bot.DB;

public abstract class DBAction
{
    public DBActionType type;
    public Action<DBActionResult> Completed;

    public InsertDBAction Insert()
    {
        return (InsertDBAction)this;
    }

    public QueryDBAction Query()
    {
        return (QueryDBAction)this;
    }
}

public class InsertDBAction : DBAction
{
    public InsertDBAction()
    {
        type = DBActionType.Insert;
    }
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