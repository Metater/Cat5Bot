namespace Cat5Bot.Helpers; //{}

public static class InteractivityHelper
{
    public static async Task CancellableCommand(InteractivityExtension interactivity, CommandContext ctx, Func<bool, Task> result)
    {
        var x = DiscordEmoji.FromName(ctx.Client, ":x:");
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == x, ctx.User, TimeSpan.FromSeconds(Constants.CancellableCommandTimeout));
        await result(!em.TimedOut);
    }
}