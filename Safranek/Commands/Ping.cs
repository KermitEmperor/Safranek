using Discord;
using Discord.Interactions;

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
}
