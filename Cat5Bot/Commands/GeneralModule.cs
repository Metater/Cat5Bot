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