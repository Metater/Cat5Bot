using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public abstract class DBActionResult
{
    public DBActionResultType type;

    public QueryDBActionResult Query()
    {
        return (QueryDBActionResult)this;
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
    Query
}