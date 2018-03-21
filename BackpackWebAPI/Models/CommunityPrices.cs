namespace BackpackWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

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
        /// If <see cref="IsSuccess"/> is false, this contains the reason for failure.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; private set; }

        /// <summary>
        /// The time this API was called, in UTC.
        /// </summary>
        public DateTime CurrentTime
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_currentTime);

        [JsonProperty("current_time")]
        private long _currentTime;

        /// <summary>
        /// The current value for the lowest form of currency in $USD.
        /// </summary>
        [JsonProperty("raw_usd_value")]
        public double RawUsdValue { get; private set; }

        /// <summary>
        /// The lowest form of currency upon which <see cref="RawUsdValue"/> is based on.
        /// </summary>
        [JsonProperty("usd_currency")]
        public string UsdCurrency { get; private set; }

        /// <summary>
        /// The definition index for the item used by <see cref="UsdCurrency"/>.
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
                if (_craftable == null)
                    return null;

                var json = _craftable.ToString();
                
                try
                {
                    return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, ItemPrice>>(json);
                    
                }
                catch
                {
                    List<ItemPrice> list = JsonConvert.DeserializeObject<List<ItemPrice>>(json);
                    return new Dictionary<string, ItemPrice>()
                    {
                        { "0", list[0] }
                    };
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
                if (_nonCraftable == null)
                    return null;
                var json = _nonCraftable.ToString();

                try
                {
                    return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, ItemPrice>>(json);

                }
                catch
                {
                    var list = JsonConvert.DeserializeObject<List<ItemPrice>>(json);
                    return new Dictionary<string, ItemPrice>()
                    {
                        { "0", list[0] }
                    };
                }
            }
        }

        [JsonProperty("Craftable")]
        private object _craftable;

        [JsonProperty("Non-Craftable")]
        private object _nonCraftable;

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
        public string CurrencyType { get; private set; } = "None";

        /// <summary>
        /// The value for this item valued in its <see cref="CurrencyType"/>.
        /// </summary>
        [JsonProperty("value")]
        public double? Value { get; private set; }

        /// <summary>
        /// The value's upper bound in price as a multiple of <see cref="CurrencyType"/>. Only set if the item has a price range.
        /// </summary>
        [JsonProperty("value_high")]
        public double? HighValue { get; private set; }

        /// <summary>
        /// <para>If getRaw was set to 1 when making this API call, this is the average between the low and high raw values for this item.</para>
        /// <para>If getRaw was set to 2 when making this API call, this is the low raw value for this item.</para>
        /// </summary>
        [JsonProperty("value_raw")]
        public double? RawValue { get; private set; }

        /// <summary>
        /// If getRaw was set to 2 when making this API call, this is the high raw value for this item.
        /// </summary>
        [JsonProperty("value_raw_high")]
        public double? RawHighValue { get; private set; }

        /// <summary>
        /// The last time this item was updated, in UTC. If it is the UTC Epoch, assume there has been no update.
        /// </summary>
        public DateTime LastUpdate
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_lastUpdate.GetValueOrDefault());

        [JsonProperty("last_update")]
        private long? _lastUpdate;

        /// <summary>
        /// <para>A relative difference between the last price and current price in the lowest currency.</para>
        /// <para>If this item is the lowest currency, then this is USD.</para>
        /// <para>If this is equal to the value, assume this is a new item.</para>
        /// <para>If the difference is 0, assume a refresh (no change).</para>
        /// </summary>
        [JsonProperty("difference")]
        public double? Difference { get; private set; }

        /// <summary>
        /// Whether or not this is for the australium variant of the weapon.
        /// </summary>
        [JsonProperty("australium")]
        public bool? IsAustralium { get; private set; }
    }
}
