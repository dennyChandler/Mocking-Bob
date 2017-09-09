using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using Newtonsoft.Json;

namespace sarcasticBob
{
    public class ScrewYou
    {
        public string Execute()
        {
            var random = new Random(DateTime.Now.Millisecond);

            return GetRandomFilePathStream(random);
        }

        private string GetRandomFilePathStream(Random random)
        {

            string apiKey;
            // first, let's load our configuration file
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            {
                var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);

                apiKey = cfgjson.GiphyApiKey;

            }
            var giphy = new Giphy(apiKey);
            var rand = new RandomParameter
            {
                Tag = "Flip off"
            };
            var result = giphy.RandomGif(rand);
            return result.Result.Data.Url;
        }
    }

    public struct ConfigJson
    {
        [JsonProperty("giphyApiKey")]
        public string GiphyApiKey { get; }
    }
}
