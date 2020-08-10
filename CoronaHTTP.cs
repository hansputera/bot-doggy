using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;

class CoronaHTTP
{
    string url = "https://disease.sh/v2/countries/";

    public async void country(string c)
    {
        var req = new HttpClient();
        var body = await req.GetAsync(url + c);
        
        if (!body.IsSuccessStatusCode)
        {
            Console.WriteLine("Could not find that country!");
            return;
        }
        else
        {
            var data = await body.Content.ReadAsStringAsync();
            var converting = JObject.Parse(data).ToObject<Corontod>();
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