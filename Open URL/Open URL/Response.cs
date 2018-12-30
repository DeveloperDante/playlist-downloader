using System.Collections.Generic;
using Newtonsoft.Json;

namespace Open_URL
{
    public struct ResponseActual
    {
        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonProperty("items")]
        public List<ItemsClass> Items { get; set; }
    }

    public struct ItemsClass
    {
        [JsonProperty("contentDetails")]
        public ContentDetailsClass ContentDetails { get; set; }
    }

    public struct ContentDetailsClass
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
    }
}
