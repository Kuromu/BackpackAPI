namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the root object for the ClassifiedsSearch API response.
    /// </summary>
    public class ClassifiedsSearchRoot
    {
        /// <summary>
        /// Message containing the reason for failure if the API does not respond with code 2XX.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; private set; }

        /// <summary>
        /// Total number of listings matched by the query.
        /// </summary>
        [JsonProperty("total")]
        public long TotalListings { get; private set; }

        /// <summary>
        /// How many listings were skipped for this page.
        /// </summary>
        [JsonProperty("skip")]
        public long Skipped { get; private set; }

        /// <summary>
        /// How many listings are shown on this page.
        /// </summary>
        [JsonProperty("page_size")]
        public long PageSize { get; private set; }

        /// <summary>
        /// Listings with intent to buy.
        /// </summary>
        /// <remarks>
        /// Also known as "buy orders" on the site.
        /// </remarks>
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

        /// <summary>
        /// Listings with intent to sell.
        /// </summary>
        /// <remarks>
        /// Also known as "sell orders" on the site.
        /// </remarks>
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


        [JsonProperty("buy")]
        private object buy;

        [JsonProperty("sell")]
        private object sell;
    }

    /// <summary>
    /// Represents listings for each intent (buy/sell).
    /// </summary>
    public class Intent
    {
        /// <summary>
        /// Total number of listings matched by the query for this intent.
        /// </summary>
        [JsonProperty("total")]
        public long TotalListings { get; private set; }

        /// <summary>
        /// All listings returned by the query.
        /// </summary>
        [JsonProperty("listings")]
        public IReadOnlyList<Listing> Listings { get; private set; }
    }

    /// <summary>
    /// Represents an individual listing.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// The listing's internal ID. Guaranteed to be unique.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// SteamID64 of the user who created the listing.
        /// </summary>
        [JsonProperty("steamid")]
        public ulong SteamId { get; private set; }

        /// <summary>
        /// WebAPI-style item object.
        /// </summary>
        /// <remarks>
        /// Will have a <see cref="BackpackWebAPI.Models.ListingItem.Name"/> property if getItemNames is set to <code>true</code>, and a <see cref="BackpackWebAPI.Models.ListingItem.MarketplacePrice"/> if the listing is a marketplace.tf cross-listing.
        /// </remarks>
        [JsonProperty("item")]
        public ListingItem Item { get; private set; }

        /// <summary>
        /// Which Steam game (AppID) this listing belongs to.
        /// </summary>
        /// <remarks>
        /// Note: AppID 440 is Team Fortress 2, AppID 570 is Dota 2, and AppID 730 is Counter-Strike: Global Offensive.
        /// </remarks>
        [JsonProperty("appid")]
        public int AppId { get; private set; }

        /// <summary>
        /// Which currencies the user is looking to buy/sell for.
        /// </summary>
        /// <remarks>
        /// Uses backpack.tf's internal currency names (eg. "metal" or "keys").
        /// </remarks>
        [JsonProperty("currencies")]
        public IReadOnlyDictionary<string, double> Prices { get; private set; }

        /// <summary>
        /// Whether the user accepts trade offers (<c>true</c>) or only adds (<c>false</c>) for this listing.
        /// </summary>
        [JsonProperty("offers")]
        public bool AllowsOffers { get; private set; }

        /// <summary>
        /// Whether the user allows negotiation (<c>true</c>) or only wants the buyout price (<c>false</c>).
        /// </summary>
        [JsonProperty("buyout")]
        public bool AllowsNegotiation { get; private set; }

        /// <summary>
        /// User-set listing comment.
        /// </summary>
        /// <remarks>
        /// Not HTML escaped.
        /// </remarks>
        [JsonProperty("details")]
        public string Details { get; private set; }

        /// <summary>
        /// UNIX timestamp representing the date this listing was created.
        /// </summary>
        [JsonProperty("created")]
        public long DateCreated { get; private set; }

        /// <summary>
        /// UNIX timestamp representing the date this listing was last bumped on the site.
        /// </summary>
        /// <remarks>
        /// If this field equals <see cref="BackpackWebAPI.Models.Listing.DateCreated"/>, then the listing has not been bumped.
        /// </remarks>
        [JsonProperty("bump")]
        public long LastBump { get; private set; }

        /// <summary>
        /// Intent of this listing - <c>0</c> = buy, <c>1</c> = sell.
        /// </summary>
        [JsonProperty("intent")]
        public int Intent { get; private set; }

        /// <summary>
        /// If <c>fold</c> was set to <c>true</c> when making this call, this is the number of listings for identical price stacked underneath this one.
        /// </summary>
        [JsonProperty("count")]
        public int? FoldedCount { get; private set; }

        /// <summary>
        /// If set, this listing is a backpack.tf Premium promoted listing.
        /// </summary>
        [JsonProperty("promoted")]
        public bool? IsPromotedListing { get; private set; }
    }

    /// <summary>
    /// Represents the item in a listing.
    /// </summary>
    public class ListingItem
    {
        /// <summary>
        /// Item ID.
        /// </summary>
        [JsonProperty("id")]
        public long? Id { get; private set; }

        /// <summary>
        /// Item original ID.
        /// </summary>
        [JsonProperty("original_id")]
        public long? OriginalId { get; private set; }

        /// <summary>
        /// Listing item definition index.
        /// </summary>
        [JsonProperty("defindex")]
        public long? DefIndex { get; private set; }

        /// <summary>
        /// Item level.
        /// </summary>
        [JsonProperty("level")]
        public int? Level { get; private set; }

        /// <summary>
        /// Item inventory ID.
        /// </summary>
        [JsonProperty("inventory")]
        public long? Inventory { get; private set; }

        /// <summary>
        /// Quantity of listing item (usually 1).
        /// </summary>
        [JsonProperty("quantity")]
        public int? Quantity { get; private set; }

        /// <summary>
        /// Listing items' origin ID.
        /// </summary>
        [JsonProperty("origin")]
        public int? Origin { get; private set; }

        /// <summary>
        /// If set, this is the item's style ID.
        /// </summary>
        [JsonProperty("style")]
        public int? Style { get; private set; }

        /// <summary>
        /// A <c>List</c> of the item's attributes and values.
        /// </summary>
        [JsonProperty("attributes")]
        public IReadOnlyList<ItemAttribute> Attributes { get; private set; }

        /// <summary>
        /// If <c>getItemNames</c> was set to <c>true</c> when making this call, this is the item's in-game name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// If this listing is a marketplace.tf cross-listing, this is the listing price on marketplace.tf.
        /// </summary>
        [JsonProperty("marketplace_price")]
        public double? MarketplacePrice { get; private set; }
    }

    /// <summary>
    /// Represents each listing item attribute.
    /// </summary>
    public class ItemAttribute
    {
        /// <summary>
        /// Definition index for this attribute. See (http://optf2.com/440/attributes)
        /// </summary>
        [JsonProperty("defindex")]
        public int DefIndex { get; private set; }

        /// <summary>
        /// Value for this attribute. See the reference for <see cref="BackpackWebAPI.Models.ItemAttribute.DefIndex"/> to see what the value represents.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; private set; }

        /// <summary>
        /// If set, this is the floating-point value for the attribute.
        /// </summary>
        [JsonProperty("float_value")]
        public double? FloatValue { get; private set; }
    }
}
