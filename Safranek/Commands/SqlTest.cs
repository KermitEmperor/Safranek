using Discord;
using Discord.WebSocket;
using Safranek.Database.Types;

namespace Safranek.Commands;

public class SqlTest : CommandBase{
    public SqlTest() {
        CommandProperties = Build();
    }
    
    public override SlashCommandProperties Build() {
        SlashCommandBuilder builder = new SlashCommandBuilder()
            .WithName("sqltest")
            .WithDescription("sql server table test")
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("option")
                .WithDescription("add/remove server to/from table")
                .WithRequired(true)
                .AddChoice("add", 1)
                .AddChoice("remove", 2)
                .WithType(ApplicationCommandOptionType.Integer));
        

        return builder.Build();
    }
    public override SlashCommandProperties CommandProperties { get; }
    public override async Task Runnable(SocketSlashCommand commandCall, Client client) {
        if (commandCall.GuildId is not null) {
            if ((long)commandCall.Data.Options.First().Value == 1) {
                Guilds.addServer(commandCall.GuildId.Value);
                await commandCall.RespondAsync($"Guild {commandCall.GuildId.Value} added to SQLite db");
            } else {
                Guilds.removeServer(commandCall.GuildId.Value);
                await commandCall.RespondAsync($"Guild {commandCall.GuildId.Value} removed from SQLite db");
            }
        } else {
            await commandCall.RespondAsync($"Failed to add guild to SQLite db (GuildId is null?)");
        }
    }
}
