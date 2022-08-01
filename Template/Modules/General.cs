using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace AverageBot.Modules
{
    public class General : ModuleBase
    {
        [Command("average")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("bot!");
        }

        [Command("info")]
        public async Task Info(SocketGuildUser user = null)
        {
            if (user == null)
                user = (SocketGuildUser)Context.User;
            var builder = new EmbedBuilder()
                    .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                    .WithDescription($"Info about {user.Username}")
                    .WithColor(new Color(68, 50, 168))
                    .AddField("User ID", user.Id, true)
                    .AddField("Created at", user.CreatedAt.ToString("MM/dd/yyyy"), true)
                    .AddField("Joined at", user.JoinedAt.Value.ToString("MM/dd/yyyy"), true)
                    .AddField("Roles", string.Join(" ", user.Roles.Select(x => x.Mention)))
                    .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("server")]
        public async Task Server()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription("Info of this server")
                .WithTitle($"{Context.Guild.Name} Information")
                .AddField("Created at", Context.Guild.CreatedAt.ToString("MM/dd/yyyy"))
                .AddField("Membercount", (Context.Guild as SocketGuild).MemberCount + " members", true);
                //.AddField("Online users", (Context.Guild as SocketGuild).Users.Where(x => x.Status != UserStatus.Offline).Count() + " members", true); //fixme
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
