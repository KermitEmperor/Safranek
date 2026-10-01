using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public class Yes : CommandBase {

    public Yes() {
        CommandProperties = Build();
    }
    
    
    public override SlashCommandProperties Build() {
        SlashCommandBuilder builder = new();
        builder.Name = "yes";
        builder.Description = "Minimal Example command";
        
        return builder.Build();
    }

    public override SlashCommandProperties CommandProperties { get; }

    public override Task Runnable(SocketSlashCommand commandCall, Client client) {
        return commandCall.RespondAsync("YES!!!!!");
    }
}
