namespace BackpackWebAPI
{
    using System;
    using System.Threading.Tasks;
    using BackpackWebAPI.Models;
    using System.Net.Http;
    using Newtonsoft.Json;
    using BackpackWebAPI.Exceptions;
    using BackpackWebAPI.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class BackpackWrapper : IBackpackWrapper
    {
        private static HttpClient _http = new HttpClient();
        private string apiKey;

        public BackpackWrapper(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<UserInfoRoot> GetUserInfoAsync(List<ulong> steamIds)
        {
            try
            {
                return await FetchResponseAsync<UserInfoRoot>($"https://backpack.tf/api/users/info/v1?steamids={string.Join(',', steamIds.Take(100).ToList())}&key={apiKey}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in GetUserInfo API - check inputs and try again.", ex);
            }
        }

        public async Task<CommunityPricesRoot> GetCommunityPricesAsync(long since = 0)
        {
            try
            {
                return await FetchResponseAsync<CommunityPricesRoot>($"https://backpack.tf/api/IGetPrices/v4?key={apiKey}{((since > 0) ? string.Empty : $"&since={since}")}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in GetCommunityPrices API - check inputs and try again.", ex);
            }
        }

        public async Task<CurrenciesRoot> GetCurrenciesAsync(int getRaw = 0)
        {
            try
            {
                return await FetchResponseAsync<CurrenciesRoot>($"https://backpack.tf/api/IGetCurrencies/v1?key={apiKey}&raw={getRaw}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in GetCurrencies API - check inputs and try again.", ex);
            }
        }

        public async Task<PriceHistoryRoot> GetPriceHistoryAsync(string item, string quality, int appId = 440, bool craftable = true, int priceindex = 0)
        {
            try
            {
                return await FetchResponseAsync<PriceHistoryRoot>($"https://backpack.tf/api/IGetPriceHistory/v1?item={item}&quality={quality}&key={apiKey}&appid={appId}&craftable={((craftable) ? 1 : 0)}&priceindex={priceindex}").ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in GetPriceHistory API - check inputs and try again.", ex);
            }
        }

        public async Task<ClassifiedsSearchRoot> SearchClassifiedsAsync(string item, bool getItemNames = false, Dictionary<string, string> filters = null, string intent = "dual", int page = 1, int pageSize = 10, bool fold = true, ulong steamId = 0)
        {
            string steamIdFormat = (steamId > 0) ? $"&steamid={steamId}" : string.Empty;
            string filtersFormat = string.Empty;
            if (filters != null)
            {
                foreach (string s in filters.Keys)
                {
                    filtersFormat += $"&{s}={filters[s]}";
                }
            }

            try
            {
                return await FetchResponseAsync<ClassifiedsSearchRoot>($"https://backpack.tf/api/classifieds/search/v1?item={item}&item_names={(getItemNames ? 1 : 0)}{filtersFormat}&intent={intent}&page={page}&page_size={pageSize}&fold={(fold ? 1 : 0)}{steamIdFormat}&key={this.apiKey}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in ClassifiedsSearch API - check inputs and try again.", ex);
            }
        }

        public async Task<SpecialItemsRoot> GetSpecialItemsAsync(int appid = 440)
        {
            try
            {
                return await FetchResponseAsync<SpecialItemsRoot>($"https://backpack.tf/api/IGetSpecialItems/v1?key={this.apiKey}&appid={appid}").ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw new BackpackRequestException("Error in GetSpecialItems API - check inputs and try again.", ex);
            }
        }

        private async Task<T> FetchResponseAsync<T>(string uri)
        {
            var response = await _http.GetStringAsync(uri).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
