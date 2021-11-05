namespace Cat5Bot.Commands; //{}

[Group("schedule"), Aliases("s"), Description("Used for scheduling attendable events.")]
public class SchedulingModule : BaseCommandModule
{
    [GroupCommand]
    public async Task Schedule(CommandContext ctx)
    {
        var interactivity = ctx.Client.GetInteractivity();
        var msg = await ctx.RespondAsync($"Would you like to add or remove an event? Timeout in 30 seconds.");
        var emoAdd = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
        var emoRemove = DiscordEmoji.FromName(ctx.Client, ":x:");
        await msg.CreateReactionsAsync(250, emoAdd, emoRemove);
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == emoAdd || xe.Emoji == emoRemove, ctx.User, TimeSpan.FromSeconds(30));
        await msg.DeleteAsync();
        if (!em.TimedOut)
        {
            if (em.Result.Emoji == emoAdd)
            {
                await ScheduleAdd(ctx);
            }
            else if (em.Result.Emoji == emoRemove)
            {
                await ScheduleRemove(ctx);
            }
        }
    }

    [Command("add"), Aliases("create", "make"), Description("Adds an event to the schedule.")]
    public async Task ScheduleAdd(CommandContext ctx)
    {
        await ctx.RespondAsync($"Added event.");
    }

    [Command("remove"), Aliases("del", "delete"), Description("Removes an event from the schedule.")]
    public async Task ScheduleRemove(CommandContext ctx)
    {
        await ctx.RespondAsync($"Removed event.");
    }

    [GroupCommand]
    public async Task Plan(CommandContext ctx, params string[] args)
    {
        // name of event
        // type of event
        // date of event
        // start time of event
        // end time of event
        await ctx.RespondAsync($"All one plan.");
    }

}