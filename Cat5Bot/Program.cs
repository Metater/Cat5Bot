using System;
using System.Threading.Tasks;
using Cat5Bot.DB;
using Cat5Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

Console.WriteLine("Hello, World!");

string token = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.secret");

var discord = new DiscordClient(new DiscordConfiguration()
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged
});

discord.UseInteractivity();

var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
{
    StringPrefixes = new[] { "!" }
});

commands.RegisterCommands<GeneralModule>();
commands.RegisterCommands<SchedulingModule>();
commands.RegisterCommands<PermissionsModule>();
commands.RegisterCommands<NamingModule>();
commands.RegisterCommands<AttendingModule>();

await discord.ConnectAsync();
while (!Console.KeyAvailable)
{
    await Task.Delay(30000);
    Cat5BotDB.I.WriteAll();
}

// update db entries only through methods
// make db entries get; private set, and update only through methods
// make type in DBEntry protexted set, public get

//https://stackoverflow.com/questions/23285753/how-to-await-on-async-delegate
//https://docs.microsoft.com/en-us/dotnet/api/system.func-2?view=net-5.0

Cat5BotDB.I.WriteAll();