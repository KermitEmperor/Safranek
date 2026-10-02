using System.Diagnostics;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace Safranek.Commands;

public class Ping : CommandBase{
    public Ping() {
        CommandProperties = Build();
    }
    public override SlashCommandProperties Build() {
        SlashCommandBuilder builder = new();
        builder.WithName("ping");
        builder.WithDescription("Replies pong! (Also gives Discord API Latency in milliseconds)");
        return builder.Build();
    }

    public override SlashCommandProperties CommandProperties { get; }
    public override async Task Runnable(SocketSlashCommand commandCall, Client client) {
        
        EmbedBuilder builder = new();
        builder.WithTitle("🏓 Pong !");
        builder.WithFooter("delay: ~" + client.Latency + "ms");
        
        await commandCall.RespondAsync(embed: builder.Build());
        
    }
}
