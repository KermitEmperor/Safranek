using Discord;
using Discord.WebSocket;

namespace Safranek.Commands;

public class PingAll : CommandBase
{

    public override SlashCommandProperties Build()
    {
        SlashCommandBuilder builder = new SlashCommandBuilder()
            .WithName("pingall")
            .WithDescription("Pings everyone by their handée");
        return builder.Build();
    }

    public override async Task Runnable(SocketSlashCommand commandCall, Client client)
    {
        EmbedBuilder builder = new EmbedBuilder();
        ulong? guildId = commandCall.GuildId;
        if (!guildId.HasValue)
        {
            builder.WithTitle("GuildID was invalid!");
            await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
            return;
        } 
        //TODO make this less stupid, like avoiding commandCall.RespondAsync two differently
        builder.WithTitle("Pinging all the people in the channel!");
        await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);

        ISocketMessageChannel channel = commandCall.Channel;
        IEnumerable<IGuildUser> users = await client.GetGuild((ulong)guildId!).GetUsersAsync().FlattenAsync();
        string mentions = "";
        foreach (IGuildUser user in users)
        {
            mentions+=user.Mention;
        }
        //TODO embed
        await channel.SendMessageAsync(mentions);
    }
}