using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public class PingAll : CommandBase
{
    public PingAll()
    {
        CommandProperties = Build();
    }

    public override SlashCommandProperties Build()
    {
        SlashCommandBuilder builder = new SlashCommandBuilder()
            .WithName("pingall")
            .WithDescription("Pings everyone by their handée");
        return builder.Build();
    }

    public override async Task Runnable(SocketSlashCommand commandCall, Client client)
    {
        EmbedBuilder builder = new EmbedBuilder()
            .WithTitle("Pinging all the people in the channel!");
        
        
        await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
        
        ISocketMessageChannel channel = commandCall.Channel;
        var users = await client.GetGuild((ulong)commandCall.GuildId).GetUsersAsync().FlattenAsync();
        string mentions = "";
        foreach (var user in users)
        {
            mentions+=user.Mention;
        }
        //TODO embed
        await channel.SendMessageAsync(mentions);
    }
}