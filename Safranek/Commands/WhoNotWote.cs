using Discord;
using Discord.WebSocket;
using Safranek.Commands.Util;

namespace Safranek.Commands;

public class WhoNotWote : CommandBase
{
    public override SlashCommandProperties Build()
    {
        SlashCommandBuilder builder = new SlashCommandBuilder()
            .WithName("whonotvote")
            .WithDescription("Tells you privately who didn't vore in a poll you replied to as a command")
            .AddOption(new SlashCommandOptionBuilder()
                    .WithName("pollid")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithDescription("Copy the poll id and paste it here as an option")
                    .WithRequired(true)
            );

        return builder.Build();
    }

    public override async Task Runnable(SocketSlashCommand commandCall, Client client)
    {
        EmbedBuilder builder = new();
        ulong? guildId = commandCall.GuildId;
        if (!guildId.HasValue)
        {
            builder.WithTitle("Could not find guild");
            builder.WithDescription($"Could not find guild {commandCall.GuildId} (or null).");
            await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
            return;
        }
        
        ulong id;
        bool succParse = ulong.TryParse((string)commandCall.Data.Options.First().Value, out id);
        if (!succParse)
        {
            builder.WithTitle("Invalid ID");
            builder.WithDescription("Id was not an ulong.");
            await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
            return;
        }

        IUserMessage? msg = await commandCall.Channel.GetMessageAsync(id) as IUserMessage;
        Poll? poll = msg?.Poll;
        if (!poll.HasValue)
        {
            builder.WithTitle("Invalid ID or Poll doesn't exit");
            builder.WithDescription($"Could not get poll or message {id}");
            await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
            return;
        }

        HashSet<string> mentions = new();
        foreach (var answer in poll.Value.Answers)
        {
            foreach (IUser user in await msg!.GetPollAnswerVotersAsync(answer.AnswerId).FlattenAsync())
            {
                mentions.Add(user.Mention);
            }
        }

        foreach (var user in await commandCall.Channel.GetUsersAsync().FlattenAsync())
        {
            if (!mentions.Contains(user.Mention) && !user.IsBot)
            {
                builder.Description += user.Mention + "\n";
            }
        }
        
        builder.WithTitle("Poll found!");
        await commandCall.RespondAsync(embed: builder.Build(), ephemeral: true);
    }
}