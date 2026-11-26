using Discord;
using Discord.WebSocket;
using Safranek.Commands;
using Safranek.Commands.Util;

namespace Safranek;

class Program {
    public static async Task Main(string[] args)
    {
        DiscordSocketConfig config = new DiscordSocketConfig
        {
            AlwaysDownloadUsers = true,
            GatewayIntents = GatewayIntents.GuildMembers | GatewayIntents.Guilds | GatewayIntents.GuildMessages,
        };
        Client client = new(Environment.GetEnvironmentVariable("TOKEN")!,  config);
        CommandsRegistry.Instance.DiscoverAndRegister();
        
        
        if (args.Contains("--regCommands")) {
            if (args.Contains("-guild")) {
                string guildId = args[args.IndexOf("-guild") + 1];
                
                
                //Kept here to avoid needless event registration
                client.Ready += () => {
                    CommandsRegistry.Instance.RegisterCommands(client, ulong.Parse(guildId));
                    Console.WriteLine("Registered successfully");
                    return Task.CompletedTask;
                };
            } else if (args.Contains("-global")) {
                CommandsRegistry.Instance.RegisterCommands(client);
            }
        }
        
        await client.Start();
    }
}
