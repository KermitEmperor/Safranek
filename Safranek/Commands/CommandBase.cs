using Discord;
using Discord.Interactions;

namespace Safranek.Commands;

public abstract class CommandBase {

    public abstract SlashCommandProperties Build();
    public abstract SlashCommandProperties CommandProperties { get; }
}
