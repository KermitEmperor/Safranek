using System.Runtime.CompilerServices;
using Discord;
using Safranek.Commands;

namespace Safranek;

class Program {
    public static async Task Main(string[] args) {
        Client client = new(Environment.GetEnvironmentVariable("TOKEN")!);
        client.AttachLogger();
        CommandsRegistry.Instance.DiscoverAndRegister();
        
        if (args.Contains("--regCommands")) {
            if (args.Contains("-guild")) {
                string guildId = args[args.IndexOf("-guild") + 1];
                
                //Guild ID for some reason doesn't exist?

                client.Ready += () => {
                    CommandsRegistry.Instance.RegisterCommands(client, ulong.Parse(guildId));
                    Console.WriteLine("Registered successfully");
                    return Task.CompletedTask;
                };
            } else if (args.Contains("-global")) {
                CommandsRegistry.Instance.RegisterCommands(client);
            }
        }



        client.SlashCommandExecuted += async (slashCommand) => {
            await CommandsRegistry.Instance.CommandHandler(slashCommand, client);
        };
        await client.Start();
    }
}
