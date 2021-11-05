namespace Cat5Bot.Helpers; //{}

public static class PermissionHelper
{
    public static bool AllowedSelf(ulong id, byte requiredPermissionLevel, out byte permissionLevelSelf, out string message)
    {
        permissionLevelSelf = GetLevel(id);
        message = "Insufficient permission level, must be true that:\n" +
        $"\tYour level ({permissionLevelSelf}) >= Required level ({requiredPermissionLevel}).";
        return permissionLevelSelf >= requiredPermissionLevel;
    }

    public static bool AllowedSelfAndGreaterThanOther(ulong id, byte requiredPermissionLevel, ulong otherId, out byte permissionLevelSelf, out byte permissionLevelOther, out string message)
    {
        bool allowedSelf = AllowedSelf(id, requiredPermissionLevel, out permissionLevelSelf, out message);
        permissionLevelOther = GetLevel(otherId);
        message += "\n\tYour level ({permissionLevelSelf}) > Their level ({permissionLevelOther}).";
        return allowedSelf && permissionLevelSelf > permissionLevelOther;
    }

    public static byte GetLevel(ulong id)
    {
        byte permissionLevel = 0;
        if (Cat5BotDB.I.Query((e) => e.Alias == id, out AliasedByteDBEntry e))
            permissionLevel = e.bite;
        return permissionLevel;
    }
}