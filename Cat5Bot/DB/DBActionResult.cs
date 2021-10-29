using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public abstract class DBActionResult
{
    public DBActionResultType type;

    public InsertDBActionResult Insert()
    {
        return (InsertDBActionResult)this;
    }

    public QueryDBActionResult Query()
    {
        return (QueryDBActionResult)this;
    }
}

public class InsertDBActionResult : DBActionResult
{
    public InsertDBActionResult()
    {
        type = DBActionResultType.Insert;
    }
}

public class QueryDBActionResult : DBActionResult
{
    public readonly DBEntry dbEntry;

    public QueryDBActionResult(DBEntry dbEntry)
    {
        type = DBActionResultType.Query;
        this.dbEntry = dbEntry;
    }
}

public enum DBActionResultType
{
    Insert,
    Query
}