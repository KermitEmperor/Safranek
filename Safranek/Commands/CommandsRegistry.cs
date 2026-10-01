using System.Reflection;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Safranek.Commands;


//Please read https://csharpindepth.com/Articles/Singleton indepth, using code that you're unfamiliar with is bad practice
public sealed class CommandsRegistry {
    //Holy shit i just pressed tab after writing "selaed" and it passed this entitre singleton here
    //I mean thats what i wanted but still damn, lemme read it atleast
    private static readonly Lazy<CommandsRegistry> Lazy = new Lazy<CommandsRegistry>(() => new CommandsRegistry());
    public static CommandsRegistry Instance { get { return Lazy.Value; } }
    private static readonly Dictionary<string,CommandBase> _commands = new();
    
    private CommandsRegistry() { }

    public void RegisterCommand(CommandBase command) {
        _commands.Add((string)command.CommandProperties.Name, command);
    }

    public IReadOnlyDictionary<string, CommandBase> GetCommands() {
        return _commands;
    }

    public void DiscoverAndRegister() {
        var commandTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(CommandBase))  && !t.IsAbstract);

        foreach (var commandType in commandTypes) {
            var command = (CommandBase)Activator.CreateInstance(commandType)!;
            RegisterCommand(command);
        }
    }

    public void RegisterCommands(Client client, ulong guildId) {
        SocketGuild? guild = client.GetGuild(guildId);
        if (guild is null) {
            Console.WriteLine($"Guild {guildId} doesn't exist");
            return;
        }
        foreach (var command in GetCommands()) {
            try {
                SlashCommandProperties commandProp = command.Value.CommandProperties;
                guild.CreateApplicationCommandAsync(commandProp);
                Console.WriteLine($"{commandProp.Name} has been registered to guild with id {guildId}");
            }
            catch (HttpException exception) {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }
    }
    
    public void RegisterCommands(Client client) {
        foreach (var command in GetCommands()) {
            try {
                client.CreateGlobalApplicationCommandAsync(command.Value.CommandProperties);
            }
            catch (HttpException exception) {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }
    }

    public async Task CommandHandler(SocketSlashCommand commandCall, Client client) {
        await _commands[commandCall.CommandName].Runnable(commandCall, client);
    }

    public async Task CommandComponentHandler(SocketMessageComponent component, Client client) {
        try {
            await _commands[component.Data.CustomId.Split("-")[0]].ComponentRunnable(component, client);
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }
    }
}
