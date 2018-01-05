namespace BackpackWebAPI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class UserInfoRoot
    {
        [JsonProperty("users")]
        public IReadOnlyDictionary<ulong, User> Users { get; private set; }
    }

    public class User
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("avatar")]
        public string Avatar { get; private set; }

        [JsonProperty("last_online")]
        public long LastOnline { get; private set; }

        [JsonProperty("admin")]
        public bool? IsAdmin { get; private set; }

        [JsonProperty("donated")]
        public double? AmountDonated { get; private set; }

        [JsonProperty("premium")]
        public bool? HasPremium { get; private set; }

        [JsonProperty("premium_months_gifted")]
        public long? PremiumMonthsGifted { get; private set; }

        [JsonProperty("integrations")]
        public Integrations Integrations { get; private set; }

        [JsonProperty("bans")]
        public Bans Bans { get; private set; }

        [JsonProperty("voting")]
        public Voting Voting { get; private set; }

        [JsonProperty("inventory")]
        public IReadOnlyDictionary<int, Inventory> Inventory { get; private set; }

        [JsonProperty("trust")]
        public Trust Trust { get; private set; }
    }

    public class Integrations
    {
        [JsonProperty("group_member")]
        public bool? IsGroupMember { get; private set; } = null;

        [JsonProperty("marketplace_seller")]
        public bool? IsMarketplaceSeller { get; private set; } = null;

        [JsonProperty("automatic")]
        public bool? IsAutomatic { get; private set; } = null;

        [JsonProperty("steamrep_admin")]
        public bool? IsSteamRepAdmin { get; private set; } = null;
    }

    public class Bans
    {
        [JsonProperty("steamrep_scammer")]
        public bool? IsSteamRepScammer { get; private set; }

        [JsonProperty("steamrep_caution")]
        public bool? IsSteamRepCaution { get; private set; }

        [JsonProperty("valve")]
        public ValveBans Valve { get; private set; }

        [JsonProperty("all")]
        public SiteBan All { get; private set; }

        [JsonProperty("suggestions")]
        public SiteBan Suggestions { get; private set; }

        [JsonProperty("comments")]
        public SiteBan Comments { get; private set; }

        [JsonProperty("trust")]
        public SiteBan Trust { get; private set; }

        [JsonProperty("issues")]
        public SiteBan Issues { get; private set; }

        [JsonProperty("classifieds")]
        public SiteBan Classifieds { get; private set; }

        [JsonProperty("customizations")]
        public SiteBan Customizations { get; private set; }

        [JsonProperty("reports")]
        public SiteBan Reports { get; private set; }
    }

    public class ValveBans
    {
        [JsonProperty("economy")]
        public bool? IsEconomyBanned { get; private set; }

        [JsonProperty("community")]
        public bool? IsCommunityBanned { get; private set; }

        [JsonProperty("vac")]
        public bool? IsVacBanned { get; private set; }

        [JsonProperty("game")]
        public bool? IsGameBanned { get; private set; }
    }

    public class SiteBan
    {
        [JsonProperty("end")]
        public long Ending { get; private set; }

        [JsonProperty("reason")]
        public string Reason { get; private set; }
    }

    public class Voting
    {
        [JsonProperty("reputation")]
        public long? Reputation { get; private set; }

        [JsonProperty("votes")]
        public Votes Votes { get; private set; }

        [JsonProperty("suggestions")]
        public Suggestions Suggestions { get; private set; }
    }

    public class Votes
    {
        [JsonProperty("positive")]
        public long? Positive { get; private set; }

        [JsonProperty("negative")]
        public long? Negative { get; private set; }

        [JsonProperty("accepted")]
        public long? Accepted { get; private set; }
    }

    public class Suggestions
    {
        [JsonProperty("created")]
        public long? Created { get; private set; }

        [JsonProperty("accepted")]
        public long? Accepted { get; private set; }

        [JsonProperty("accepted_unusual")]
        public long? AcceptedUnusual { get; private set; }
    }

    public class Inventory
    {
        [JsonProperty("ranking")]
        public long Ranking { get; private set; }

        [JsonProperty("value")]
        public double Value { get; private set; }

        [JsonProperty("updated")]
        public long LastUpdate { get; private set; }

        [JsonProperty("metal")]
        public double Metal { get; private set; }

        [JsonProperty("keys")]
        public long Keys { get; private set; }

        [JsonProperty("slots")]
        public Slots Slots { get; private set; }
    }

    public class Slots
    {
        [JsonProperty("used")]
        public long Used { get; private set; }

        [JsonProperty("total")]
        public long Total { get; private set; }
    }

    public class Trust
    {
        [JsonProperty("positive")]
        public long Positive { get; private set; }

        [JsonProperty("negative")]
        public long Negative { get; private set; }
    }
}
