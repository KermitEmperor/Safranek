using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Safranek.Commands;

public abstract class CommandBase {

    public abstract SlashCommandProperties Build();
    public abstract SlashCommandProperties CommandProperties { get; }
    public abstract Task Runnable(SocketSlashCommand commandCall, Client client);
    public virtual Task ComponentRunnable(SocketMessageComponent component, Client client) {
        throw new NotImplementedException();
    }
}
