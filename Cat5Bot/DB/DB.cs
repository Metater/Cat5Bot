using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.DB;

public class DB : IDBSerializable
{

    private Dictionary<ulong, AliasedStringDBEntry> nameAliases = new();
    private Dictionary<ulong, AttendanceDBEntry> attendance = new();

    public DB()
    {
        // make arrays longer than ushort by storing length not in default place, make new method or store length with int
    }

    public bool InsertNameAlias(ulong alias, string name, out AliasedStringDBEntry entry)
    {
        entry = new AliasedStringDBEntry(alias, name);
        if (!nameAliases.TryAdd(alias, entry))
            return QueryNameAlias(alias, out entry);
        return true;
    }

    public bool QueryNameAlias(ulong alias, out AliasedStringDBEntry entry)
    {
        return nameAliases.TryGetValue(alias, out entry);
    }

    public bool InsertAttendance(ulong attendee, out AttendanceDBEntry entry)
    {
        entry = new AttendanceDBEntry(attendee);
        if (!attendance.TryAdd(attendee, entry))
            return QueryAttendance(attendee, out entry);
        return true;
    }

    public bool QueryAttendance(ulong attendee, out AttendanceDBEntry entry)
    {
        return attendance.TryGetValue(attendee, out entry);
    }

    public void Serialize(DBWriter dbWriter)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(DBReader dbReader)
    {
        throw new NotImplementedException();
    }
}