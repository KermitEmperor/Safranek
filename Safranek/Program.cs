using System.Runtime.CompilerServices;
using Discord;
using Safranek.Commands;

namespace Safranek;

class Program {
    public static async Task Main(string[] args) {
        Client client = new(Environment.GetEnvironmentVariable("TOKEN")!);
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
