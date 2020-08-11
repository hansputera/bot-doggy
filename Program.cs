using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Schema;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class Program
{
    private DiscordSocketClient _client;
    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    public async Task MainAsync()
    {
        OperatingSystem os = Environment.OSVersion;
        var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(_config);

        await _client.LoginAsync(TokenType.Bot, "");
        await _client.StartAsync();
        
        _client.MessageReceived += MessageReceived;
        _client.Ready += () =>
        {
            Console.WriteLine("Bot sudah sambung!");
           
            return Task.CompletedTask;
        };

        await Task.Delay(-1);
    }

    
    private async Task MessageReceived(SocketMessage message)
    {
        if (message.Content.ToLower() == "d!stats")
        {
            var statsEmbedBuild = new EmbedBuilder()
            {
                Color = new Color(0xcfa),
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = _client.CurrentUser.GetAvatarUrl().ToString(),
                    Name = _client.CurrentUser.Username
                },
                Title = "Statistics " + _client.CurrentUser.Username,
                Footer = new EmbedFooterBuilder()
                {
                    Text = "© " + System.DateTime.Now.Year
                }
              
            };
            var ownerInfo = _client.GetUser(426712723108134923);

            statsEmbedBuild.AddField("🚀 Bot Information", "**- Name: " + _client.CurrentUser.Username + "#" + _client.CurrentUser.Discriminator.ToString() + "\n- Created: " + _client.CurrentUser.CreatedAt + "\n- ID" + _client.CurrentUser.Id+ "**", true);
            statsEmbedBuild.AddField("🚶 Owner Information", "**- Name: " + ownerInfo.Username + "#" + ownerInfo.Discriminator.ToString() + "\n- Created: " + ownerInfo.CreatedAt + "\n- ID: " + ownerInfo.Id + "**");
            statsEmbedBuild.AddField("💉 System", "**- Platform: " + Environment.OSVersion.Platform + "\n- Version: " + Environment.OSVersion.VersionString + "**");

            var statsembed = statsEmbedBuild.Build();
            await message.Channel.SendMessageAsync(embed: statsembed);
        }

        if (message.Content == "<@!" + _client.CurrentUser.Id + ">" ||
            message.Content == "<@" + _client.CurrentUser.Id + ">")
        {
            await message.Channel.SendMessageAsync("Hello " + message.Author.Username + " my prefix is `d!`");
        }

        if (message.Content.ToLower() == "d!guilds")
        {
            var d = _client.Guilds.Count;
          await message.Channel.SendMessageAsync("Currently i'm in " + d + " guilds.");
        }
       
        if (message.Content.ToLower() == "d!date")
        {
            var date = System.DateTime.Today.Day + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year;
            await message.Channel.SendMessageAsync("Today : " + date);
        }


        if (message.Content.ToLower() == "d!ping")
        {
            await message.Channel.SendMessageAsync(":ping_pong: Pong! " + _client.Latency + "ms");
        }
        if (message.Content.ToLower() == "d!help")
        {
            var helpEmbedBuild = new EmbedBuilder()

            {
                Color = new Color(2984),
                Author = new EmbedAuthorBuilder()
                {
                    Name = message.Author.Username,
                    IconUrl = message.Author.GetAvatarUrl().ToString()
                },
                Title = "Help commands for " + _client.CurrentUser.Username,
                Description = "Thank you for using Doggy Bot!",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Discord.NET Example Bot"
                }
            };
            helpEmbedBuild.AddField("😄 General", "help, stats, ping, date, guilds", true);
            helpEmbedBuild.AddField("🔩 Support", "**[Saweria](https://saweria.co/hanifdwyputra)**", true);

            var helpembed = helpEmbedBuild.Build();
            await message.Channel.SendMessageAsync(embed: helpembed);
        }
    }
}
