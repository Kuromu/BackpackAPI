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

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // THIS API HAS A COOLDOWN OF 60 SECONDS, IF NOT MORE. COMMENT THE LINE BELOW IF YOU WILL BE RUNNING MULTIPLE TESTS IN QUICK SUCCESSION.
            CommunityPricesRoot communityPrices = await apiWrapper.GetCommunityPricesAsync().ConfigureAwait(false);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ClassifiedsSearchRoot root = await apiWrapper.GetClassifiedsSearch("Scattergun", filters: new Dictionary<string, string>{ {"quality", "5" } });

            await Task.Delay(-1);
        }
    }
}
