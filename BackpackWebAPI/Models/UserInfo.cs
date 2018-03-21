namespace BackpackWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the root object for the IGetUserInfo API response.
    /// </summary>
    public class UserInfoRoot
    {
        /// <summary>
        /// An <c>IReadOnlyDictionary</c> with SteamID64s as keys.
        /// </summary>
        [JsonProperty("users")]
        public IReadOnlyDictionary<ulong, User> Users { get; private set; }
    }

    /// <summary>
    /// Represents an individual user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's profile name. This is usually also their Steam name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// User's avatar image URL.
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; private set; }

        /// <summary>
        /// Time the user last visited this site, in UTC.
        /// </summary>
        public DateTime LastOnline
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_lastOnline);

        [JsonProperty("last_online")]
        private long _lastOnline;

        /// <summary>
        /// Whether or not the user is a backpack.tf admin.
        /// </summary>
        [JsonProperty("admin")]
        public bool? IsAdmin { get; private set; }

        /// <summary>
        /// Amount (in $USD) the user has donated to the site.
        /// </summary>
        [JsonProperty("donated")]
        public double? AmountDonated { get; private set; }

        /// <summary>
        /// Whether or not the user has backpack.tf Premium.
        /// </summary>
        [JsonProperty("premium")]
        public bool? HasPremium { get; private set; }

        /// <summary>
        /// Number of months of backpack.tf Premium this user has gifted to other users.
        /// </summary>
        [JsonProperty("premium_months_gifted")]
        public long? PremiumMonthsGifted { get; private set; }

        /// <summary>
        /// Site integrations for this user.
        /// </summary>
        [JsonProperty("integrations")]
        public Integrations Integrations { get; private set; }

        /// <summary>
        /// Site (and Valve) bans for this user.
        /// </summary>
        [JsonProperty("bans")]
        public Bans Bans { get; private set; }

        /// <summary>
        /// Voting and suggestion statistics for this user.
        /// </summary>
        [JsonProperty("voting")]
        public Voting Voting { get; private set; }

        /// <summary>
        /// An <c>IReadOnlyDictionary</c> with Steam game AppIDs as keys.
        /// </summary>
        [JsonProperty("inventory")]
        public IReadOnlyDictionary<int, Inventory> Inventory { get; private set; }

        /// <summary>
        /// Site trust stats for this user.
        /// </summary>
        [JsonProperty("trust")]
        public Trust Trust { get; private set; }
    }

    /// <summary>
    /// Represents site integrations for this user.
    /// </summary>
    public class Integrations
    {
        /// <summary>
        /// Whether or not the user is a member of the Meet the Stats Steam group.
        /// </summary>
        [JsonProperty("group_member")]
        public bool? IsGroupMember { get; private set; }

        /// <summary>
        /// Whether or not the user is a seller on marketplace.tf.
        /// </summary>
        [JsonProperty("marketplace_seller")]
        public bool? IsMarketplaceSeller { get; private set; }

        /// <summary>
        /// Whether or not the user is currently online with backpack.tf Automatic.
        /// </summary>
        [JsonProperty("automatic")]
        public bool? IsAutomatic { get; private set; }

        /// <summary>
        /// Whether or not the user is a SteamRep Admin.
        /// </summary>
        [JsonProperty("steamrep_admin")]
        public bool? IsSteamRepAdmin { get; private set; }
    }

    /// <summary>
    /// All ban info for a user.
    /// </summary>
    public class Bans
    {
        /// <summary>
        /// Whether or not the user is marked as "SteamRep Scammer".
        /// </summary>
        [JsonProperty("steamrep_scammer")]
        public bool? IsSteamRepScammer { get; private set; }

        /// <summary>
        /// Whether or not the user is marked as "SteamRep Scammer".
        /// </summary>
        [JsonProperty("steamrep_caution")]
        public bool? IsSteamRepCaution { get; private set; }

        /// <summary>
        /// The user's Valve bans, if any.
        /// </summary>
        [JsonProperty("valve")]
        public ValveBans Valve { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from all services.
        /// </summary>
        [JsonProperty("all")]
        public SiteBan All { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from suggestions.
        /// </summary>
        [JsonProperty("suggestions")]
        public SiteBan Suggestions { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from making comments.
        /// </summary>
        [JsonProperty("comments")]
        public SiteBan Comments { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from using trust.
        /// </summary>
        [JsonProperty("trust")]
        public SiteBan Trust { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from making issues.
        /// </summary>
        [JsonProperty("issues")]
        public SiteBan Issues { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from classifieds.
        /// </summary>
        [JsonProperty("classifieds")]
        public SiteBan Classifieds { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from user customizations.
        /// </summary>
        [JsonProperty("customizations")]
        public SiteBan Customizations { get; private set; }

        /// <summary>
        /// If not null, this user has been banned from reports.
        /// </summary>
        [JsonProperty("reports")]
        public SiteBan Reports { get; private set; }
    }

    /// <summary>
    /// Represents a user's Valve bans.
    /// </summary>
    public class ValveBans
    {
        /// <summary>
        /// Whether or not the user has a Valve economy ban.
        /// </summary>
        [JsonProperty("economy")]
        public bool? IsEconomyBanned { get; private set; }

        /// <summary>
        /// Whether or not the user has a Valve community ban.
        /// </summary>
        [JsonProperty("community")]
        public bool? IsCommunityBanned { get; private set; }

        /// <summary>
        /// Whether or not the user has a Valve VAC ban.
        /// </summary>
        [JsonProperty("vac")]
        public bool? IsVacBanned { get; private set; }

        /// <summary>
        /// Whether or not the user has a Valve game ban.
        /// </summary>
        [JsonProperty("game")]
        public bool? IsGameBanned { get; private set; }
    }

    /// <summary>
    /// Represents an individual backpack.tf site ban.
    /// </summary>
    public class SiteBan
    {
        /// <summary>
        /// Time until the ban ends. If -1, the ban is permanent.
        /// </summary>
        /// <remarks>
        /// I am unsure whether this is in months, or a timestamp.
        /// </remarks>
        [JsonProperty("end")]
        public long Ending { get; private set; }

        /// <summary>
        /// The reason for the ban being in place.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; private set; }
    }

    /// <summary>
    /// Represents a user's voting and suggestion stats.
    /// </summary>
    public class Voting
    {
        /// <summary>
        /// User's site reputation score.
        /// </summary>
        [JsonProperty("reputation")]
        public long? Reputation { get; private set; }

        /// <summary>
        /// User's voting stats.
        /// </summary>
        [JsonProperty("votes")]
        public Votes Votes { get; private set; }

        /// <summary>
        /// User's suggestion stats.
        /// </summary>
        [JsonProperty("suggestions")]
        public PriceSuggestions Suggestions { get; private set; }
    }

    /// <summary>
    /// Represents a user's voting stats.
    /// </summary>
    public class Votes
    {
        /// <summary>
        /// Number of positive votes the user has submitted.
        /// </summary>
        [JsonProperty("positive")]
        public long? Positive { get; private set; }

        /// <summary>
        /// Number of negative votes the user has submitted.
        /// </summary>
        [JsonProperty("negative")]
        public long? Negative { get; private set; }

        /// <summary>
        /// Number of votes the user made that were accepted.
        /// </summary>
        [JsonProperty("accepted")]
        public long? Accepted { get; private set; }
    }

    /// <summary>
    /// Represents a user's price suggestion stats.
    /// </summary>
    public class PriceSuggestions
    {
        /// <summary>
        /// Number of price suggestions a user has created.
        /// </summary>
        [JsonProperty("created")]
        public long? Created { get; private set; }

        /// <summary>
        /// Number of price suggestions a user has created that were accepted.
        /// </summary>
        [JsonProperty("accepted")]
        public long? Accepted { get; private set; }

        /// <summary>
        /// Number of unusual price suggestions a user has created that were accepted.
        /// </summary>
        [JsonProperty("accepted_unusual")]
        public long? AcceptedUnusual { get; private set; }
    }

    /// <summary>
    /// Represents a user's inventory stats for a specific game.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// User's backpack.tf ranking position for this game.
        /// </summary>
        [JsonProperty("ranking")]
        public long Ranking { get; private set; }

        /// <summary>
        /// User's inventory value for this game; this is in "ref" for TF2 and $USD for Dota 2 and CS:GO.
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; private set; }

        /// <summary>
        /// The last time this user's inventory for this game was updated on the site.
        /// </summary>
        public DateTime LastUpdate
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_lastUpdate);

        [JsonProperty("updated")]
        private long _lastUpdate;

        /// <summary>
        /// Amount of raw metal the user has in their inventory for this game.
        /// </summary>
        [JsonProperty("metal")]
        public double Metal { get; private set; }

        /// <summary>
        /// Number of raw keys the user has in their inventory for this game.
        /// </summary>
        [JsonProperty("keys")]
        public long Keys { get; private set; }

        /// <summary>
        /// User's inventory slots for this game.
        /// </summary>
        [JsonProperty("slots")]
        public Slots Slots { get; private set; }
    }

    /// <summary>
    /// Represents a user's inventory slots for this game.
    /// </summary>
    public class Slots
    {
        /// <summary>
        /// Used inventory slots.
        /// </summary>
        [JsonProperty("used")]
        public long Used { get; private set; }

        /// <summary>
        /// Total inventory slots for this game.
        /// </summary>
        [JsonProperty("total")]
        public long Total { get; private set; }
    }

    /// <summary>
    /// Represents a user's trust stats.
    /// </summary>
    public class Trust
    {
        /// <summary>
        /// Number of positive trust votes for this user.
        /// </summary>
        [JsonProperty("positive")]
        public long Positive { get; private set; }

        /// <summary>
        /// Number of negative trust votes for this user.
        /// </summary>
        [JsonProperty("negative")]
        public long Negative { get; private set; }
    }
}
