namespace BackpackWebAPITester
{
    using BackpackWebAPI;
    using BackpackWebAPI.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class Program
    {
        static void Main(string[] args)
            => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            // your API key goes here.
            Wrapper apiWrapper = new Wrapper("");

            CurrenciesRoot currencies = await apiWrapper.GetCurrenciesAsync().ConfigureAwait(false);
            PriceHistoryRoot priceHistory = await apiWrapper.GetPriceHistoryAsync("Mann Co. Supply Crate Key", "Unique").ConfigureAwait(false);
            UserInfoRoot userInfo = await apiWrapper.GetUserInfoAsync(new List<ulong> { 76561198051696861 }).ConfigureAwait(false);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            //   THIS API HAS A COOLDOWN OF 60 SECONDS! UNCOMMENT THE LINE BELOW IF YOU WOULD LIKE TO TEST THIS API.
            // CommunityPricesRoot communityPrices = await apiWrapper.GetCommunityPricesAsync().ConfigureAwait(false);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            ClassifiedsSearchRoot classifiedsSearch = await apiWrapper.GetClassifiedsSearchAsync("Scattergun", page: 2, filters: new Dictionary<string, string>{ {"quality", "5" } }).ConfigureAwait(false);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // THIS API HAS A COOLDOWN OF 1800 SECONDS! UNCOMMENT THE LINE BELOW IF YOU WOULD LIKE TO TEST THIS API.
            // SpecialItemsRoot specialItems = await apiWrapper.GetSpecialItemsAsync().ConfigureAwait(false);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            await Task.Delay(-1);
        }
    }
}
