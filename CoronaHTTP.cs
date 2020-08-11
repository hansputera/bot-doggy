using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class CoronaHTTP
{
    public static async void country(string c, SocketMessage message)
    {
        var req = new HttpClient();
        var body = await req.GetAsync("https://disease.sh/v2/countries/" + c.ToLower());
        
        if (!body.IsSuccessStatusCode)
        {
            Console.WriteLine("Could not find that country!");
            return;
        }
        else
        {
            var data = await body.Content.ReadAsStringAsync();
            var converting = JObject.Parse(data).ToObject<Corontod>();

            var coronaEmbedBuild = new EmbedBuilder()
            {
                Color = new Color(23658),
                Title = "Corona info for " + c.ToLower(),
                Author = new EmbedAuthorBuilder()
                {
                    Name = converting.Country,
                    IconUrl = converting.CountryInfo.flag 
                },
                Footer = new EmbedFooterBuilder()
                {
                    Text = "© " + System.DateTime.Now.Year

                }
            };
            coronaEmbedBuild.AddField("😷 Cases", "**" + converting.cases + " Cases**", true);
            coronaEmbedBuild.AddField("😰 Deaths", "**" + converting.deaths + " Deaths**", true);
            coronaEmbedBuild.AddField("🙏 Recovered", "**" + converting.recovered + " Recovered**", true);
            coronaEmbedBuild.AddField("🔍 Continent", "**" + converting.continent + "**", true);

            await message.Channel.SendMessageAsync(embed: coronaEmbedBuild.Build());

            
        }
        
    }
    private struct Corontod
    {
        [JsonProperty("updated")]
        public int Updated { get; private set; }
        [JsonProperty("country")]
        public string Country { get; private set; }
        [JsonProperty("countryInfo")]
        public countryinfo CountryInfo { get; private set; }
        
        [JsonProperty("cases")]
        public int cases { get; private set; }

        [JsonProperty("todayCases")]
        public int todayCases { get; private set; }

        [JsonProperty("deaths")]
        public int deaths { get; private set; }

        [JsonProperty("todayDeaths")]
        public int todayDeaths { get; private set; }

        [JsonProperty("recovered")]
        public int recovered { get; private set; }

        [JsonProperty("todayRecovered")]
        public int todayRecovered { get; private set; }

        [JsonProperty("active")]
        public int active { get; private set; }

        [JsonProperty("critical")]
        public int critical { get; private set; }
    
        [JsonProperty("casesPerOneMillion")]
        public int casesPerOneMillion { get; private set; }

        [JsonProperty("deathsPerOneMillion")]
        public int deathsPerOneMillion { get; private set; }

        [JsonProperty("tests")]
        public int tests { get; private set; }

        [JsonProperty("testsPerOneMillion")]
        public int testsPerOneMillion { get; private set; }

        [JsonProperty("population")]
        public int population { get; private set; }

        [JsonProperty("continent")]
        public string continent { get; private set; }


    }

    public struct countryinfo
    {
        [JsonProperty("_id")]
        public int _id { get; private set; }
        [JsonProperty("iso2")]
        public string iso2 { get; private set; }
        [JsonProperty("iso3")]
        public string iso3 { get; private set; }

        [JsonProperty("lat")]
        public int lat { get; private set; }

        [JsonProperty("long")]
        public int Long { get; private set; }

        [JsonProperty("flag")]
        public string flag { get; private set; }
    }
}
