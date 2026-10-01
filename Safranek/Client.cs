using Discord;
using Discord.WebSocket;

namespace Safranek;

public class Client(string token) : DiscordSocketClient {

    public void AttachLogger() {
        Log += message => {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }; 
    }
    

    public async Task Start() {
        await LoginAsync(TokenType.Bot, token);
        await StartAsync();
        
        await Task.Delay(-1);
    }

}
