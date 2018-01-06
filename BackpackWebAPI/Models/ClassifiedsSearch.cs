namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ClassifiedsSearchRoot
    {
        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("total")]
        public long TotalListings { get; private set; }

        [JsonProperty("skip")]
        public long Skipped { get; private set; }

        [JsonProperty("page_size")]
        public long PageSize { get; private set; }

        public Intent Buy
        {
            get
            {
                if (buy == null)
                    return null;

                var json = buy.ToString();

                try
                {
                    return JsonConvert.DeserializeObject<Intent>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
            //=> (buy != null && !JArray.Parse(buy.ToString()).HasValues) ? JsonConvert.DeserializeObject<Intent>(buy.ToString()) : null;

        public Intent Sell
        {
            get
            {
                if (sell == null)
                    return null;

                var json = sell.ToString();

                try
                {
                    return JsonConvert.DeserializeObject<Intent>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
            //=> (sell != null && !JArray.Parse(sell.ToString()).HasValues) ? JsonConvert.DeserializeObject<Intent>(sell.ToString()) : null;


        [JsonProperty("buy")]
        private object buy;

        [JsonProperty("sell")]
        private object sell;
    }

    public class Intent
    {
        [JsonProperty("total")]
        public long TotalListings { get; private set; }

        [JsonProperty("listings")]
        public IReadOnlyList<Listing> Listings { get; private set; }
    }

    public class Listing
    {
        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("steamid")]
        public ulong SteamId { get; private set; }

        [JsonProperty("item")]
        public ListingItem Item { get; private set; }

        [JsonProperty("appid")]
        public int AppId { get; private set; }

        [JsonProperty("currencies")]
        public IReadOnlyDictionary<string, double> Prices { get; private set; }

        [JsonProperty("offers")]
        public bool AllowsOffers { get; private set; }

        [JsonProperty("buyout")]
        public bool AllowsNegotiation { get; private set; }

        [JsonProperty("details")]
        public string Details { get; private set; }

        [JsonProperty("created")]
        public long DateCreated { get; private set; }

        [JsonProperty("bump")]
        public long LastBump { get; private set; }

        [JsonProperty("intent")]
        public int Intent { get; private set; }

        [JsonProperty("count")]
        public int? FoldedCount { get; private set; }

        [JsonProperty("promoted")]
        public bool? IsPromotedListing { get; private set; }
    }

    public class ListingItem
    {
        [JsonProperty("id")]
        public long? Id { get; private set; }

        [JsonProperty("original_id")]
        public long? OriginalId { get; private set; }

        [JsonProperty("defindex")]
        public long? DefIndex { get; private set; }

        [JsonProperty("level")]
        public int? Level { get; private set; }

        [JsonProperty("inventory")]
        public long? Inventory { get; private set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; private set; }

        [JsonProperty("origin")]
        public int? Origin { get; private set; }

        [JsonProperty("style")]
        public int? Style { get; private set; }

        [JsonProperty("attributes")]
        public IReadOnlyList<ItemAttribute> Attributes { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
    }

    public class ItemAttribute
    {
        [JsonProperty("defindex")]
        public int DefIndex { get; private set; }

        [JsonProperty("value")]
        public string Value { get; private set; }

        [JsonProperty("float_value")]
        public double? FloatValue { get; private set; }
    }
}
