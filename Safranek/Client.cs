using Discord;
using Discord.WebSocket;

namespace Safranek;

public class Client(string token) {
    private DiscordSocketClient _socketClient = new();
    private readonly string _token = token;
    
    public DiscordSocketClient SocketClient => _socketClient;

    public void AttachLogger() {
        _socketClient.Log += message => {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }; 
    }
    

    public async Task Start() {
        await _socketClient.LoginAsync(TokenType.Bot, token);
        await _socketClient.StartAsync();
        
        await Task.Delay(-1);
    }
}
