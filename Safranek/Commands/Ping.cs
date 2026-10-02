using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public class Ping : CommandBase{
    public Ping() {
        CommandProperties = Build();
    }
    public sealed override SlashCommandProperties Build() {
        SlashCommandBuilder builder = new SlashCommandBuilder()
            .WithName("ping")
            .WithDescription("Replies pong! (Also gives Discord API Latency in milliseconds)");
        return builder.Build();
    }

    public override SlashCommandProperties CommandProperties { get; }
    public override async Task Runnable(SocketSlashCommand commandCall, Client client) {
        EmbedBuilder builder = new EmbedBuilder()
            .WithTitle("🏓 Pong !")
            .WithFooter("delay: ~" + client.Latency + "ms");
        
        await commandCall.RespondAsync(embed: builder.Build());
    }
}
