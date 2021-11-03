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
using Cat5Bot;
using Cat5Bot.DB;
using Cat5Bot.Helpers;

namespace Cat5Bot.Commands;

[Group("name"), Aliases("n", "link", "l"), Description("Used for linking user's names to their account.")]
public class NamingModule : BaseCommandModule
{
    [GroupCommand, Description("Links your full name to attendance record.")]
    public async Task NameSelf(CommandContext ctx, [Description("Your full name to be used in the attendance record")] params string[] fullName)
    {
        if (fullName.Length < 1)
        {
            await ctx.RespondAsync($"Please put your full name following the command.");
            return;
        }
        string nameStr = string.Join(" ", fullName);
        Cat5BotDB.LinkAttendee(ctx.User.Id, nameStr);
        await ctx.RespondAsync($"Linked your attendance record to \"{nameStr}\".");
    }

    [GroupCommand, Description("Links someone's full name to their attendance record.")]
    public async Task NameOther(CommandContext ctx, [Description("Person you are linking")] DiscordUser user, [Description("Their full name to be used in the attendance record")] params string[] fullName)
    {
        if (PermissionHelper.AllowedSelfAndGreaterThanOther(ctx.User.Id, Constants.Permission.NameOther, user.Id, out _, out _, out string message))
        {
            if (fullName.Length < 1)
            {
                await ctx.RespondAsync($"Please put their full name following the command.");
                return;
            }
            string nameStr = string.Join(" ", fullName);
            Cat5BotDB.LinkAttendee(user.Id, nameStr);
            await ctx.RespondAsync($"Linked their attendance record to \"{nameStr}\".");
        }
        else
        {
            await ctx.RespondAsync(message);
        }
    }
}