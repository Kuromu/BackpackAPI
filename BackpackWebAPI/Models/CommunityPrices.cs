namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class CommunityPricesRoot
    {
        [JsonProperty("response")]
        public CommunityPricesResponse Response { get; private set; } 
    }

    public class CommunityPricesResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("current_time")]
        public long CurrentTime { get; private set; }

        [JsonProperty("raw_usd_value")]
        public double RawUsdValue { get; private set; }

        [JsonProperty("usd_currency")]
        public string UsdCurrency { get; private set; }

        [JsonProperty("usd_currency_index")]
        public long UsdCurrencyIndex { get; private set; }

        [JsonProperty("items")]
        public IReadOnlyDictionary<string, Item> Items { get; private set; }
    }

    public class Item
    {
        [JsonProperty("defindex")]
        public IReadOnlyList<long> DefIndexes { get; private set; }

        [JsonProperty("prices")]
        public IReadOnlyDictionary<string, Quality> Qualities { get; private set; }
    }

    public class Quality
    {
        [JsonProperty("Tradable")]
        public Tradability Tradable { get; private set; }
    }

    public class Tradability
    {
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

    public class ItemPrice
    {
        [JsonProperty("currency")]
        public string CurrencyType { get; private set; }

        [JsonProperty("value")]
        public double Value { get; private set; }

        [JsonProperty("value_high")]
        public double? ValueHigh { get; private set; }

        [JsonProperty("value_raw")]
        public double? ValueRaw { get; private set; }

        [JsonProperty("value_raw_high")]
        public double? ValueRawHigh { get; private set; }

        [JsonProperty("last_update")]
        public long LastUpdate { get; private set; }

        [JsonProperty("difference")]
        public double Difference { get; private set; }

        [JsonProperty("australium")]
        public bool? Australium { get; private set; }
    }
}
