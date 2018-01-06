namespace BackpackWebAPI.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CurrenciesRoot
    {
        [JsonProperty("response")]
        public CurrenciesResponse Response { get; private set; }
    }

    public class CurrenciesResponse
    { 
        [JsonProperty("success")]
        public bool IsSuccess { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("currencies")]
        public IReadOnlyDictionary<string, Currency> Currencies { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }
    }

    public class Currencies
    {
        [JsonProperty("metal")]
        public Currency Metal;

        [JsonProperty("hat")]
        public Currency Hat;

        [JsonProperty("keys")]
        public Currency Keys;

        [JsonProperty("earbuds")]
        public Currency Earbuds;
    }

    public class Currency
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("quality")]
        public long Quality { get; private set; }

        [JsonProperty("priceindex")]
        public string PriceIndex { get; private set; }

        [JsonProperty("single")]
        public string Single { get; private set; }

        [JsonProperty("plural")]
        public string Plural { get; private set; }

        [JsonProperty("round")]
        public long? Round { get; private set; }

        [JsonProperty("blanket")]
        public long? Blanket { get; private set; }

        [JsonProperty("craftable")]
        public string Craftable { get; private set; }

        [JsonProperty("tradable")]
        public string Tradable { get; private set; }

        [JsonProperty("defindex")]
        public long Defindex { get; private set; }

        [JsonProperty("price")]
        public CurrencyPrice Price { get; private set; }
    }

    public class CurrencyPrice
    {
        [JsonProperty("value")]
        public double Value { get; private set; }

        [JsonProperty("currency")]
        public string CurrencyType { get; private set; }

        [JsonProperty("difference")]
        public double Difference { get; private set; }

        [JsonProperty("last_update")]
        public long LastUpdate { get; private set; }

        [JsonProperty("value_high")]
        public double? HighValue { get; private set; }

        [JsonProperty("value_raw")]
        public double? RawValue { get; private set; }

        [JsonProperty("value_high_raw")]
        public double? HighRawValue { get; private set; }
    }
}
