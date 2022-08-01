using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Discord;


namespace AverageBot.Modules
{
    public class fun : ModuleBase
    {
        [Command("umass")]
        [Alias("reddit")]
        public async Task umass(string subreddit = null) 
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.reddit.com/r/{subreddit ?? "umass"}/random.json?limit=1");
            if (!result.StartsWith("["))
            {
                await Context.Channel.SendMessageAsync("This subreddit doesn't exist");
                return;
            }
            JArray arr = JArray.Parse(result);
            JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());

            var builder = new EmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new Color(189, 4, 4))
                .WithTitle(post["title"].ToString())
                .WithUrl("https://reddit.com" + post["permalink"].ToString())
                .WithFooter($"🗨 {post["num_comments"]} ⬆️ {post["ups"]}");
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }        
    }
}