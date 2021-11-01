using Cat5Bot.DB;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat5Bot.Helpers;

public static class PermissionHelper
{
    public static bool Allowed(ulong id, byte requiredPermissionLevel, out byte permissionLevel)
    {
        permissionLevel = GetLevel(id);
        return permissionLevel >= requiredPermissionLevel;
    }

    public static byte GetLevel(ulong id)
    {
        byte permissionLevel = 0;
        if (Cat5BotDB.I.Query((e) => e.alias == id, out AliasedByteDBEntry e))
            permissionLevel = e.bite;
        return permissionLevel;
    }
}