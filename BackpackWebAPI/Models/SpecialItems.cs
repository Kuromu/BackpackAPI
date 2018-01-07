namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the root object for the IGetSpecialItems API response.
    /// </summary>
    public class SpecialItemsRoot
    {
        /// <summary>
        /// The main response data for this API.
        /// </summary>
        [JsonProperty("response")]
        public SpecialItemsResponse Response { get; private set; }
    }

    /// <summary>
    /// Represents the main response data for this API.
    /// </summary>
    public class SpecialItemsResponse
    {
        /// <summary>
        /// Whether or not the query was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// UNIX timestamp representing the time this API was called.
        /// </summary>
        [JsonProperty("current_time")]
        public long CurrentTime { get; private set; }

        /// <summary>
        /// <c>List</c> of special items.
        /// </summary>
        [JsonProperty("items")]
        public IReadOnlyList<SpecialItem> Items { get; private set; }
    }

    /// <summary>
    /// Represents an individual special item.
    /// </summary>
    public class SpecialItem
    {
        /// <summary>
        /// Internal backpack.tf name for this item.
        /// </summary>
        [JsonProperty("name")]
        public string InternalName { get; private set; }

        /// <summary>
        /// In-game name for this item.
        /// </summary>
        [JsonProperty("item_name")]
        public string ItemName { get; private set; }

        /// <summary>
        /// Definition index for this item.
        /// </summary>
        [JsonProperty("defindex")]
        public long? DefIndex { get; private set; }

        /// <summary>
        /// In-game item class, such as <c>tf_wearable</c>.
        /// </summary>
        [JsonProperty("item_class")]
        public string Class { get; private set; }

        /// <summary>
        /// In-game item type.
        /// </summary>
        [JsonProperty("item_type_name")]
        public string TypeName { get; private set; }

        /// <summary>
        /// Internal backpack.tf description for this item.
        /// </summary>
        [JsonProperty("item_description")]
        public string Description { get; private set; }

        /// <summary>
        /// In-game item quality.
        /// </summary>
        [JsonProperty("item_quality")]
        public int? Quality { get; private set; }

        /// <summary>
        /// Minimum iLevel for this item.
        /// </summary>
        /// <remarks>
        /// I actually don't know what this means. It may have to do with in-game "item levels".
        /// </remarks>
        [JsonProperty("min_ilevel")]
        public int? MinLevel { get; private set; }

        /// <summary>
        /// Maximum iLevel for this item.
        /// </summary>
        /// <remarks>
        /// I actually don't know what this means. It may have to do with in-game "item levels".
        /// </remarks>
        [JsonProperty("max_ilevel")]
        public int? MaxLevel { get; private set; }

        /// <summary>
        /// In-game image URL for this item.
        /// </summary>
        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }

        /// <summary>
        /// Larger in-game image URL for this item.
        /// </summary>
        [JsonProperty("image_url_large")]
        public string ImageUrlLarge { get; private set; }

        /// <summary>
        /// Steam game AppID this item belongs to.
        /// </summary>
        [JsonProperty("appid")]
        public int? AppId { get; private set; }
    }
}
