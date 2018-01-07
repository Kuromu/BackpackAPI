namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents the root object for the IGetPrices API.
    /// </summary>
    public class CommunityPricesRoot
    {
        /// <summary>
        /// The main response data for this API.
        /// </summary>
        [JsonProperty("response")]
        public CommunityPricesResponse Response { get; private set; } 
    }

    /// <summary>
    /// Represents the main response data for this API.
    /// </summary>
    public class CommunityPricesResponse
    {
        /// <summary>
        /// Whether or not the query was a success.
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// If <see cref="BackpackWebAPI.Models.CommunityPricesResponse.IsSuccess"/> is false, this contains the reason for failure.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; private set; }

        /// <summary>
        /// UNIX timestamp representing the time this API was called.
        /// </summary>
        [JsonProperty("current_time")]
        public long CurrentTime { get; private set; }

        /// <summary>
        /// The current value for the lowest form of currency in $USD.
        /// </summary>
        [JsonProperty("raw_usd_value")]
        public double RawUsdValue { get; private set; }

        /// <summary>
        /// The lowest form of currency upon which <see cref="BackpackWebAPI.Models.CommunityPricesResponse.RawUsdValue"/> is based on.
        /// </summary>
        [JsonProperty("usd_currency")]
        public string UsdCurrency { get; private set; }

        /// <summary>
        /// The definition index for the item used by <see cref="BackpackWebAPI.Models.CommunityPricesResponse.UsdCurrency"/>.
        /// </summary>
        [JsonProperty("usd_currency_index")]
        public long UsdCurrencyIndex { get; private set; }

        /// <summary>
        /// A dictionary of all community priced items, with the item names as keys.
        /// </summary>
        [JsonProperty("items")]
        public IReadOnlyDictionary<string, Item> Items { get; private set; }
    }

    /// <summary>
    /// Represents and individual item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The item's possible definition indexes. Some items have multiple.
        /// </summary>
        [JsonProperty("defindex")]
        public IReadOnlyList<long> DefIndexes { get; private set; }

        /// <summary>
        /// All community priced qualities for this item. (see https://redd.it/5gez3b for the corresponding IDs for qualities.)
        /// </summary>
        [JsonProperty("prices")]
        public IReadOnlyDictionary<string, Quality> Qualities { get; private set; }
    }

    /// <summary>
    /// Represents an item's different quality sub-categories
    /// </summary>
    public class Quality
    {
        /// <summary>
        /// The item's tradability sub-category; this is hard-coded to "Tradable" as non-tradable items don't have pricing data.
        /// </summary>
        [JsonProperty("Tradable")]
        public Tradability Tradable { get; private set; }
    }

    /// <summary>
    /// Represents an item's tradability sub-category.
    /// </summary>
    public class Tradability
    {
        /// <summary>
        /// A dictionary of item prices for the Craftable variant of the item, with priceindexes as keys.
        /// </summary>
        /// <remarks>
        /// If there is only one valid priceindex for the item, its priceindex is 0.
        /// </remarks>
        [JsonIgnore]
        public IReadOnlyDictionary<string, ItemPrice> Craftable
        {
            get
            {
                if (this.craftable == null)
                    return null;

                var json = this.craftable.ToString();
                
                try
                {
                    return new Dictionary<string, ItemPrice>()
                    {
                        { "0", JsonConvert.DeserializeObject<ItemPrice>(JArray.Parse(json).First.ToString()) }
                    };
                }
                catch
                {
                    return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, ItemPrice>>(json);
                }
            }
        }

        /// <summary>
        /// A dictionary of item prices for the Non-Craftable variant of the item, with priceindexes as keys.
        /// </summary>.
        /// <remarks>
        /// If there is only one valid priceindex for the item, its priceindex is 0.
        /// </remarks>
        [JsonIgnore]
        public IReadOnlyDictionary<string, ItemPrice> NonCraftable
        {
            get
            {
                if (this.nonCraftable == null)
                    return null;
                var json = this.nonCraftable.ToString();

                try
                {
                    return new Dictionary<string, ItemPrice>()
                    {
                        { "0", JsonConvert.DeserializeObject<ItemPrice>(JArray.Parse(json).First.ToString()) }
                    };
                }
                catch
                {
                    return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, ItemPrice>>(json);
                }
            }
        }

        [JsonProperty("Craftable")]
        private object craftable;

        [JsonProperty("Non-Craftable")]
        private object nonCraftable;

    }

    /// <summary>
    /// Represents an item price for a given priceindex.
    /// </summary>
    public class ItemPrice
    {
        /// <summary>
        /// The type of currency used for pricing this item.
        /// </summary>
        [JsonProperty("currency")]
        public string CurrencyType { get; private set; }

        /// <summary>
        /// The value for this item as a multiple of <see cref="BackpackWebAPI.Models.ItemPrice.CurrencyType"/>.
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; private set; }

        /// <summary>
        /// The value's upper bound in price as a multiple of <see cref="BackpackWebAPI.Models.ItemPrice.CurrencyType"/>. Only set if the item has a price range.
        /// </summary>
        [JsonProperty("value_high")]
        public double? ValueHigh { get; private set; }

        /// <summary>
        /// <para>If getRaw was set to 1 when making this API call, this is the average between the low and high raw values for this item.</para>
        /// <para>If getRaw was set to 2 when making this API call, this is the low raw value for this item.</para>
        /// </summary>
        [JsonProperty("value_raw")]
        public double? ValueRaw { get; private set; }

        /// <summary>
        /// If getRaw was set to 2 when making this API clal, this is the high raw value for this item.
        /// </summary>
        [JsonProperty("value_raw_high")]
        public double? ValueRawHigh { get; private set; }

        /// <summary>
        /// UNIX timestamp representing this item's last pricing update.
        /// </summary>
        [JsonProperty("last_update")]
        public long LastUpdate { get; private set; }

        /// <summary>
        /// <para>A relative difference between the last price and current price in the lowest currency.</para>
        /// <para>If this item is the lowest currency, then this is USD.</para>
        /// <para>If this is equal to the value, assume this is a new item.</para>
        /// <para>If the difference is 0, assume a refresh (no change).</para>
        /// </summary>
        [JsonProperty("difference")]
        public double Difference { get; private set; }

        /// <summary>
        /// Whether or not this is for the australium variant of the weapon.
        /// </summary>
        [JsonProperty("australium")]
        public bool? Australium { get; private set; }
    }
}
