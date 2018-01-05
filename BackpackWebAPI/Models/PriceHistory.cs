namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
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
        private IReadOnlyList<PriceHistory> priceHistory;

        public IReadOnlyDictionary<long, PriceHistory> PriceHistory
        {
            get
            {
                Dictionary<long, PriceHistory> history = new Dictionary<long, PriceHistory>();
                foreach (PriceHistory p in this.priceHistory)
                {
                    history.Add(p.Timestamp, p);
                }
                return history;
            }
        }

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
