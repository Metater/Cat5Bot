using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public abstract class DBActionResult
{
    public DBActionResultType type;
}

public class VoidDBActionResult : DBActionResult
{
    public VoidDBActionResult()
    {
        type = DBActionResultType.Void;
    }
}

public class EntryDBActionResult : DBActionResult
{
    public readonly DBEntry dbEntry;

    public EntryDBActionResult(DBEntry dbEntry)
    {
        type = DBActionResultType.Entry;
        this.dbEntry = dbEntry;
    }
}

public enum DBActionResultType
{
    Void,
    Entry
}