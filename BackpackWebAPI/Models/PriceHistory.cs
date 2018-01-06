namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class PriceHistoryRoot
    {
        [JsonProperty("response")]
        internal PriceHistoryResponse Response { get; private set; }
    }

    public class PriceHistoryResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("history")]
        public IReadOnlyList<PriceHistory> PriceHistory { get; private set; }

        /* Disabling this for now - it's neat, but the price histories are already sorted by date
        public IReadOnlyDictionary<long, PriceHistory> PriceHistory
            => priceHistory.ToDictionary(price => price.Timestamp, price => price);
        */

    }

    public class PriceHistory
    {
        [JsonProperty("value")]
        public double Value { get; private set; }

        [JsonProperty("value_high")]
        public double? ValueHigh { get; private set; }

        [JsonProperty("currency")]
        public string CurrencyType { get; private set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; private set; }
    }
}
