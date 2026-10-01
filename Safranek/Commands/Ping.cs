using System.Diagnostics;
using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public class Ping : CommandBase{
    public Ping() {
        CommandProperties = Build();
    }
    public override SlashCommandProperties Build() {
        var builder = new SlashCommandBuilder();
        builder.WithName("ping");
        builder.WithDescription("Replies pong!");
        return builder.Build();
    }

    public override SlashCommandProperties CommandProperties { get; }
    public override async Task Runnable(SocketSlashCommand commandCall, Client client) {
        await commandCall.Channel.TriggerTypingAsync();
        var delay = Stopwatch.StartNew();
        await commandCall.RespondAsync("🏓 Pong !");
        var response = await commandCall.GetOriginalResponseAsync();
        delay.Stop();
        await response.ModifyAsync(m => m.Content = "🏓  Pong !\n*delay: "+delay.ElapsedMilliseconds+"ms*");

    }
}
