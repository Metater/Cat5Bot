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

[Group("name"), Aliases("n", "link", "l"), Description("Used for linking user's names to their account.")]
public class NamingModule : BaseCommandModule
{
    [GroupCommand]
    public async Task LinkSelf(CommandContext ctx, [Description("Your full name to be used in the attendance record")] params string[] fullName)
    {
    }
}