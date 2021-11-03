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

public class GeneralModule : BaseCommandModule
{
    [Command("attend"), Description("Marks your attendance for an event.")]
    public async Task Attend(CommandContext ctx)
    {
        string res = $"Marked as attending \"NameOfEvent\" on \"DateOfEvent\" at \"TimeOfEvent\".";
        string timeoutMsg = $"Hit the X if this is incorrect, timeout in 30 seconds.";
        var msg = await ctx.RespondAsync(res + "\n\n" + timeoutMsg);
        var x = DiscordEmoji.FromName(ctx.Client, ":x:");
        await msg.CreateReactionAsync(x);
        var interactivity = ctx.Client.GetInteractivity();
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == x, ctx.User, TimeSpan.FromSeconds(30));
        if (!em.TimedOut)
        {
            await msg.DeleteAsync();
            await ctx.RespondAsync($"Okay, corrected your attendance, here is a list of events happening soon:");
        }
        else
        {
            await msg.DeleteOwnReactionAsync(x);
            await msg.ModifyAsync(res);
        }
    }

    [Command("dbw"), Description("Writes the DB to files.")]
    public async Task DBWrite(CommandContext ctx)
    {
        byte permissionLevelRequired = 16;
        if (PermissionHelper.Allowed(ctx.User.Id, permissionLevelRequired, out byte permissionLevel))
        {
            Cat5BotDB.I.WriteAll();
            await ctx.RespondAsync($"Wrote DB.");
        }
        else
        {
            await ctx.RespondAsync($"Insufficient permission level {permissionLevel}, required >= {permissionLevelRequired}.");
        }
    }
}