using Safranek.Commands;
using Safranek.Commands.Util;
using Safranek.Database;
using Safranek.Database.Types;

namespace Safranek;

class Program {
    public static async Task Main(string[] args) {
        Client client = new(Environment.GetEnvironmentVariable("TOKEN")!);
        CommandsRegistry.Instance.DiscoverAndRegister();
        await Db.Instance.GetConnection().OpenAsync();
        Db.Instance.TableRegistration();
        
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
