namespace BackpackWebAPI.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BackpackWebAPI.Models;

    public interface IWrapper
    {
        Task<UserInfoRoot> GetUserInfoAsync(List<ulong> steamIds);

        Task<CommunityPricesRoot> GetCommunityPricesAsync(long since = 0);

        Task<CurrenciesRoot> GetCurrenciesAsync(int getRaw = 0);

        Task<PriceHistoryRoot> GetPriceHistoryAsync(string item, string quality, int appId = 440, bool craftable = true, int priceindex = 0);

        Task<ClassifiedsSearchRoot> GetClassifiedsSearch(string item, bool getItemNames = false, Dictionary<string, string> filters = null, string intent = "dual", int page = 1, int pageSize = 10, bool fold = true, ulong steamId = 0);
    }
}