namespace Cat5Bot.Helpers; //{}

public static class InteractivityHelper
{
    public static async Task CancellableCommand(string message, InteractivityExtension interactivity, CommandContext ctx, Func<bool, Task> result)
    {
        var msg = await ctx.RespondAsync(message + $"\n\nHit the X to cancel, timeout in {Constants.CancellableCommandTimeout} seconds.");
        var x = DiscordEmoji.FromName(ctx.Client, ":x:");
        await msg.CreateReactionAsync(x);
        var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == x && xe.Message == msg, ctx.User, TimeSpan.FromSeconds(Constants.CancellableCommandTimeout));
        bool cancelled = !em.TimedOut;
        if (cancelled)
        {
            await msg.DeleteAsync();
        }
        else
        {
            await msg.DeleteOwnReactionAsync(x);
            await msg.ModifyAsync(message);
        }
        await result(cancelled);
    }
}