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

namespace Cat5Bot.Commands;

public class GeneralModule : BaseCommandModule
{
    [Command("link"), Description("Links full name to attendance record")]
    public async Task LinkCommand(CommandContext ctx, [Description("Your full name to be used in the attendance record")] params string[] fullName)
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

    [Command("plan"), Description("Schedules an event")]
    public async Task PlanCommand(CommandContext ctx, string name, string type, string date, string start, string end)
    {

    }

    [Command("attend"), Description("Marks your attendance for an event")]
    public async Task AttendCommand(CommandContext ctx)
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

    [Command("dbw"), Description("Writes the DB to files")]
    public async Task DBWriteCommand(CommandContext ctx)
    {
        Cat5BotDB.WriteAll();
        await ctx.RespondAsync($"Wrote DB.");
    }
}