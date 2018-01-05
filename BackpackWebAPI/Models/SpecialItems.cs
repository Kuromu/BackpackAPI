namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SpecialItemsRoot
    {
        [JsonProperty("response")]
        public SpecialItemsResponse Response { get; private set; }
    }

    public class SpecialItemsResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        [JsonProperty("current_time")]
        public long CurrentTime { get; private set; }

        [JsonProperty("items")]
        public IReadOnlyList<SpecialItem> Items { get; private set; }
    }

    public class SpecialItem
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("item_name")]
        public string ItemName { get; private set; }

        [JsonProperty("defindex")]
        public long? DefIndex { get; private set; }

        [JsonProperty("item_class")]
        public string Class { get; private set; }

        [JsonProperty("item_type_name")]
        public string TypeName { get; private set; }

        [JsonProperty("item_description")]
        public string Description { get; private set; }

        [JsonProperty("item_quality")]
        public int? Quality { get; private set; }

        [JsonProperty("min_ilevel")]
        public int? MinLevel { get; private set; }

        [JsonProperty("max_ilevel")]
        public int? MaxLevel { get; private set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }

        [JsonProperty("image_url_large")]
        public string ImageUrlLarge { get; private set; }

        [JsonProperty("appid")]
        public int? AppId { get; private set; }
    }
}
