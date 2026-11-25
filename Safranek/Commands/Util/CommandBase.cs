using Discord;
using Discord.WebSocket;

namespace Safranek.Commands.Util;

public abstract class CommandBase {
    
    public abstract SlashCommandProperties Build(); 
    public abstract Task Runnable(SocketSlashCommand commandCall, Client client);
    public virtual Task ComponentRunnable(SocketMessageComponent component, Client client) {
        throw new NotImplementedException();
    }
}
