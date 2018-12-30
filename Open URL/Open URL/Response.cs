using System.Collections.Generic;
using Newtonsoft.Json;

namespace Open_URL
{
    public struct ResponseActual
    {
        [JsonProperty("nextPageToken")]
        public string NextPageToken;

        [JsonProperty("items")]
        public List<ItemsClass> Items;
    }

    public struct ItemsClass
    {
        [JsonProperty("contentDetails")]
        public ContentDetailsClass ContentDetails;
    }

    public struct ContentDetailsClass
    {
        [JsonProperty("videoId")]
        public string VideoId;
    }
}
