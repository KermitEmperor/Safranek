using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public abstract class CommandBase {
    protected CommandBase()
    {
        CommandProperties = Build();
    }
    public abstract SlashCommandProperties Build();
    public virtual SlashCommandProperties CommandProperties { get; set; }
    public abstract Task Runnable(SocketSlashCommand commandCall, Client client);
    public virtual Task ComponentRunnable(SocketMessageComponent component, Client client) {
        throw new NotImplementedException();
    }
}
