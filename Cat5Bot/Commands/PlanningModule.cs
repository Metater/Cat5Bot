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

[Group("plan")]
[Description("Used for planning attendable events.")]
[Aliases("schedule", "create")]
public class PlanningModule : BaseCommandModule
{
    [GroupCommand]
    public async Task PlanGroupCommand(CommandContext ctx)
    {

    }
}