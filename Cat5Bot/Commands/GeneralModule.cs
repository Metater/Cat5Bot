namespace Cat5Bot.Commands; //{}

public class GeneralModule : BaseCommandModule
{

    [Command("dbw"), Description("Writes the DB to files.")]
    public async Task DBWrite(CommandContext ctx)
    {
        if (PermissionHelper.AllowedSelf(ctx.User.Id, Constants.Permissions.WriteDB, out byte _, out string message))
        {
            Cat5BotDB.I.WriteAll();
            await ctx.RespondAsync($"Wrote DB.");
        }
        else
        {
            await ctx.RespondAsync(message);
        }
    }
}