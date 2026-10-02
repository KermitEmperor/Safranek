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
        builder.WithDescription("Replies pong!");
        return builder.Build();
    }

    public override SlashCommandProperties CommandProperties { get; }
    public override async Task Runnable(SocketSlashCommand commandCall, Client client) {
        //Discord already displays typing, but its useful when expecting an edit
        //await commandCall.Channel.TriggerTypingAsync();
        Stopwatch delay = Stopwatch.StartNew();
        
        EmbedBuilder builder = new();
        builder.WithTitle("🏓 Pong !");
        builder.WithFooter("waiting...");

        
        await commandCall.RespondAsync(embed: builder.Build());
        
        RestInteractionMessage response = await commandCall.GetOriginalResponseAsync();
        
        delay.Stop();
        
        builder.WithFooter("delay: ~" + delay.ElapsedMilliseconds + "ms");
        await response.ModifyAsync(m => m.Embed = builder.Build());
        
    }
}
