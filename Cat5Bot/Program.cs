using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

Console.WriteLine("Hello, World!");

await MainAsync();

static async Task MainAsync()
{
    string? token = Console.ReadLine();

    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = token,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged
    });

    discord.MessageCreated += async (s, e) =>
    {
        await e.Message.CreateReactionAsync(DiscordEmoji.FromName(discord, ":thumbup:"));
    };

    var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    });

    //commands.RegisterCommands<Commands.GeneralModule>();

    await discord.ConnectAsync();
    await Task.Delay(-1);
}