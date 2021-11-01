using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using Cat5Bot.DB;
using Cat5Bot.Helpers;

namespace Cat5Bot.Commands;

[Group("perms"), Aliases("p", "permissions"), Description("Used for permission actions.")]
public class PermissionsModule : BaseCommandModule
{
    [GroupCommand, Description("Gets the permissions of a user.")]
    public async Task GetPermissionLevel(CommandContext ctx, DiscordUser user)
    {
        await ctx.RespondAsync($"Permissions of \"{user.Username}\" are at level {PermissionHelper.GetLevel(user.Id)}.");
    }

    [GroupCommand, Description("Sets the permissions of a user.")]
    public async Task SetPermissionLevel(CommandContext ctx, DiscordUser user, byte userPermissionLevel)
    {
        byte permissionLevelRequired = 128;
        if (PermissionHelper.Allowed(ctx.User.Id, permissionLevelRequired, out byte permissionLevel) && userPermissionLevel < permissionLevel)
        {
            Cat5BotDB.UpdatePermission(user.Id, userPermissionLevel);
            await ctx.RespondAsync($"Updated permissions of \"{user.Username}\" to {userPermissionLevel}");
        }
        else
        {
            await ctx.RespondAsync($"Insufficient permission level {permissionLevel}, required >= {permissionLevelRequired} and {userPermissionLevel} < your permission level {permissionLevel}.");
        }
    }
}