namespace BackpackWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the root object of the IGetPriceHistory API response.
    /// </summary>
    public class PriceHistoryRoot
    {
        /// <summary>
        /// The main response data for this API.
        /// </summary>
        [JsonProperty("response")]
        public PriceHistoryResponse Response { get; private set; }
    }

    /// <summary>
    /// Represents the main response data for this API.
    /// </summary>
    public class PriceHistoryResponse
    {
        /// <summary>
        /// Whether or not the query was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// If <see cref="BackpackWebAPI.Models.PriceHistoryResponse.IsSuccess"/> is set to <c>false</c>, this contains the reason for failure.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; private set; }

        /// <summary>
        /// <c>List</c> containing the price histories for this item, sorted oldest (first) to newest (last).
        /// </summary>
        [JsonProperty("history")]
        public IReadOnlyList<PriceHistory> PriceHistory { get; private set; }

        /* Disabling this for now - it's neat, but the price histories are already sorted by date
        public IReadOnlyDictionary<long, PriceHistory> PriceHistory
            => priceHistory.ToDictionary(price => price.Timestamp, price => price);
        */

    }

    /// <summary>
    /// Represents a single instance of an item's price history.
    /// </summary>
    public class PriceHistory
    {
        /// <summary>
        /// Low price for this instance.
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; private set; }

        /// <summary>
        /// High price for this instance.
        /// </summary>
        [JsonProperty("value_high")]
        public double? ValueHigh { get; private set; }

        /// <summary>
        /// Type of currency for this instance. Uses backpack.tf's internal currency names.
        /// </summary>
        [JsonProperty("currency")]
        public string CurrencyType { get; private set; }

        /// <summary>
        /// UNIX timestamp for this instance.
        /// </summary>
        public DateTime Timestamp
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_timestamp);

        [JsonProperty("timestamp")]
        private long _timestamp;
    }
}
