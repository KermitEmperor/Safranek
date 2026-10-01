using Discord;
using Discord.WebSocket;
using Safranek.Commands;

namespace Safranek;

public class Client : DiscordSocketClient {
    private readonly string _token;

    public Client(string token) : base() {
        _token = token;
        AttachLogger();
        AttachSlashCommandHandler();
    }

    private void AttachLogger() {
        Log += message => {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }; 
    }

    private void AttachSlashCommandHandler() {
        SlashCommandExecuted += async (slashCommand) => {
            await CommandsRegistry.Instance.CommandHandler(slashCommand, this);
        };
    } 
    

    public async Task Start() {
        await LoginAsync(TokenType.Bot, _token);
        await StartAsync();
        
        await Task.Delay(-1);
    }

}
