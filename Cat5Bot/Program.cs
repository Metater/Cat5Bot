﻿using System;
using System.Threading.Tasks;
using Cat5Bot.DB;
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

/*
discord.MessageCreated += async (s, e) =>
{
    await e.Message.CreateReactionAsync(DiscordEmoji.FromName(discord, ":men_wrestling:"));
};
*/

var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
{
    StringPrefixes = new[] { "!" }
});

commands.RegisterCommands<Cat5Bot.Commands.GeneralModule>();
commands.RegisterCommands<Cat5Bot.Commands.SchedulingModule>();

await discord.ConnectAsync();
while (!Console.KeyAvailable)
{
    await Task.Delay(10000);
    Cat5BotDB.I.WriteAll();
}