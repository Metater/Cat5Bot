namespace Cat5Bot.Helpers; //{}

public static class InteractivityHelper
{
    public async Task CancellableCommand(InteractivityExtension interactivity, DiscordUser from, Func<bool, Task> done)
    {
        var x = DiscordEmoji.FromName(ctx.Client, ":x:");
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == x, ctx.User, TimeSpan.FromSeconds(Constants.CancellableCommandTimeout));
        await done(!em.TimedOut);
    }
}