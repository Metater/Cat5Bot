namespace Cat5Bot.Commands; //{}

[Group("attend"), Aliases("a"), Description("Used for attendance actions.")]
public class AttendingModule : BaseCommandModule
{
    [GroupCommand, Description("Marks your attendance for an event occuring today.")]
    public async Task AttendSelf(CommandContext ctx)
    {
        // find event today, if multiple resolve ambiguity through interactivity

        DBHelper.GetFutureEvents(5, out List<EventDBEntry> events);       string message = $"Marked as attending \"NameOfEvent\" on \"DateOfEvent\" at \"TimeOfEvent\".";
        var interactivity = ctx.Client.GetInteractivity();
        await InteractivityHelper.CancellableCommand(message, interactivity, ctx, async (cancelled) =>
        {
            if (cancelled)
            {
                await ctx.RespondAsync($"Okay, corrected your attendance, here is a list of events happening soon:");
            }
        });
    }

    [GroupCommand, Description("Marks someone else's attendance for an event occuring today.")]
    public async Task AttendOther(CommandContext ctx, DiscordUser user)
    {
        // find event today, if multiple resolve ambiguity through interactivity

        string res = $"Marked as attending \"NameOfEvent\" on \"DateOfEvent\" at \"TimeOfEvent\".";
        string timeoutMsg = $"Hit the X if this is incorrect, timeout in 120 seconds.";
        var msg = await ctx.RespondAsync(res + "\n\n" + timeoutMsg);
        var x = DiscordEmoji.FromName(ctx.Client, ":x:");
        await msg.CreateReactionAsync(x);
        var interactivity = ctx.Client.GetInteractivity();
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == x, ctx.User, TimeSpan.FromSeconds(120));
        if (!em.TimedOut)
        {
            await msg.DeleteAsync();
            await ctx.RespondAsync($"Okay, corrected their attendance, here is a list of events happening soon:");
        }
        else
        {
            await msg.DeleteOwnReactionAsync(x);
            await msg.ModifyAsync(res);
        }
    }

    /*
    private async Task Attend()
    {

    }
    */

    // cmd: attend self date: ctx, date
    // respond interactively to resolve ambiguity with timeout

    // cmd: attend other date: ctx, user, date
    // respond interactively to resolve ambiguity with timeout

    // other stuff correcting old attendance with admin or approval of person with correct permissions
}