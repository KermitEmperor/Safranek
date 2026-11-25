using Discord;
using Discord.WebSocket;
using Safranek.Commands.Util;

namespace Safranek.Commands;

public class Yes : CommandBase {
    
    public sealed override SlashCommandProperties Build() {
        SlashCommandBuilder builder = new();
        //builder.WithName() can also work here
        builder.Name = "yes";
        builder.Description = "Minimal Example command";
        
        return builder.Build();
    }

    public override Task Runnable(SocketSlashCommand commandCall, Client client) {
        return commandCall.RespondAsync("YES!!!!!");
    }
}
