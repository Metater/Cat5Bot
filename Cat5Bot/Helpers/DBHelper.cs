using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.Helpers;

public static class DBHelper
{
    public static void LinkAttendee(ulong attendee, string name)
    {
        if (Cat5BotDB.I.Query((e) => e.Alias == attendee, out AliasedStringDBEntry e))
            e.str = name;
        else
            Cat5BotDB.I.Insert(new AliasedStringDBEntry(attendee, name));
    }



    public static void UpdatePermission(ulong member, byte permission)
    {
        if (Cat5BotDB.I.Query((e) => e.Alias == member, out AliasedByteDBEntry e))
            e.bite = permission;
        else
            Cat5BotDB.I.Insert(new AliasedByteDBEntry(member, permission));
    }



    public static void AddEvent(string eventName, string eventType, DateTime time, TimeSpan length)
    {
        Cat5BotDB.I.Insert(new EventDBEntry((ulong)DateTime.Now.ToFileTime(), eventName, eventType, time, length));
    }
    public static void GetEvents(DateTime date, out List<EventDBEntry> events)
    {
        Cat5BotDB.I.Query((e) => e.time.Date == date.Date, out events);
    }
    public static void GetTodaysEvents(out List<EventDBEntry> events)
    {
        GetEvents(DateTime.Today, out events);
    }
    public static void GetEventsRange(DateTime startDate, DateTime endDate, out List<EventDBEntry> events)
    {
        Cat5BotDB.I.Query((e) => e.time.Date >= startDate.Date && e.time.Date <= endDate.Date, out events);
    }
    public static void GetFutureEvents(int count, out List<EventDBEntry> events)
    {
        if (count <= 0)
        {
            events = null;
            return;
        }

        DateTime today = DateTime.Today;
        int eventsFound = 0;
        Cat5BotDB.I.Query((e) =>
        {
            if (e.time.Date >= today && eventsFound < count)
            {
                count++;
                return true;
            }
            return false;
        }, out events);
    }
}
