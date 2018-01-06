namespace BackpackWebAPI
{
    using System;
    using System.Threading.Tasks;
    using BackpackWebAPI.Models;
    using System.Net.Http;
    using Newtonsoft.Json;
    using BackpackWebAPI.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Used for creating a wrapper for backpack.tf's web API.
    /// </summary>
    public class BackpackWrapper
    {
        private static HttpClient _http = new HttpClient();
        private string apiKey;

        /// <summary>
        /// Creates a new wrapper for the backpack.tf web API.
        /// </summary>
        /// <remarks>
        /// (To get your own API key, visit https://backpack.tf/developer/apikey/view/)
        /// </remarks>
        /// <param name="apiKey">Your backpack.tf API key.</param>
        public BackpackWrapper(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Gets user information for a List of users using the IGetUserInfo v1 API.
        /// </summary>
        /// <remarks>
        /// This API has no cooldown.
        /// </remarks>
        /// <param name="steamIds">A List of SteamID64s. Maximum of 100 per API call.</param>
        /// <returns>The root object for the IGetUserInfo API response.</returns>
        /// <exception cref="BackpackRequestException">Thrown if the input parameters are incorrect or an API key is not present.</exception>
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

        /// <summary>
        /// Gets all community prices using the IGetPrices v4 API.
        /// </summary>
        /// <remarks>
        /// This API has a cooldown of 60 seconds.
        /// </remarks>
        /// <param name="since">Only get prices which are newer than this UNIX timestamp.</param>
        /// <returns>The root object for the IGetPrices API response.</returns>
        /// <exception cref="BackpackRequestException">Thrown if the input parameters are incorrect or an API key is not present.</exception>
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

        /// <summary>
        /// Gets all backpack.tf internal currencies using the IGetCurrencies v1 API.
        /// </summary>
        /// <remarks>
        /// This API has a cooldown of 120 seconds.
        /// </remarks>
        /// <param name="getRaw">
        /// <para>If <c>getRaw</c> is set to 0, only <c>Value</c> and <c>HighValue</c> will hold values.</para>
        /// <para>If <c>getRaw</c> is set to 1, adds a <c>RawValue</c> property which represents the value of the item in the lowest currency without rounding.</para>
        /// <para>If <c>getRaw</c> is set to 2, adds a <c>HighRawValue</c> property which represents the high value of the item in the lowest currency without rounding.</para>
        /// </param>
        /// <returns>The root object for the IGetCurrencies API response.</returns>
        /// <exception cref="BackpackRequestException">Thrown if the input parameters are incorrect or an API key is not present.</exception>
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

        /// <summary>
        /// Gets price history for a specific item using the IGetPriceHistory v1 API.
        /// </summary>
        /// <remarks>
        /// This API has no cooldown.
        /// </remarks>
        /// <param name="item">The item to get price history for. Can be an item name or definition index.</param>
        /// <param name="quality">The quality of the item to get price history for. Can be a quality name or definition index.</param>
        /// <param name="appId">The Steam App ID to return data for. Valid values are: 440, 570, 730.</param>
        /// <param name="craftable">Whether to search for the Craftable or Non-Craftable variant of the item.</param>
        /// <param name="priceindex">The priceindex of the item. See https://backpack.tf/api/docs/IGetPrices for more information on priceindexes.</param>
        /// <returns>The root object for the IGetPriceHistory API response.</returns>
        /// <exception cref="BackpackRequestException">Thrown if the input parameters are incorrect or an API key is not present.</exception>
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

        /// <summary>
        /// Search backpack.tf classifieds using the ClassifiedsSearch v1 API.
        /// </summary>
        /// <remarks>
        /// This API has no cooldown, but abuse will result in your API key being revoked.
        /// </remarks>
        /// <param name="item">The item name to search for.</param>
        /// <param name="getItemNames">If set to <c>true</c>, adds a <c>Name</c> property to each item in the result.</param>
        /// <param name="filters">
        /// <para>A <c>Dictionary</c> of search filters. Tinker with searches on https://backpack.tf/classifieds to see possible filters to use.</para>
        /// <para>Note that these are not subject to any guarantee and should be used as the website dictates.</para>
        /// </param>
        /// <param name="intent">The intent for the search. Valid values are: "dual", "buy", sell".</param>
        /// <param name="page">The page number to search on. Affected by <paramref name="pageSize"/>.</param>
        /// <param name="pageSize">The size of "pages" to use. Affects <paramref name="page"/>. Must be between 1 and 30.</param>
        /// <param name="fold">If set to false, items with identical price will not be grouped together or (folded).</param>
        /// <param name="steamId">If set, only search for listings by this SteamID64.</param>
        /// <returns>The root object for the ClassifiedsSearch API response.</returns>
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

        /// <summary>
        /// Gets backpack.tf internal special items using the IGetSpecialItems v1 API.
        /// </summary>
        /// <remarks>
        /// This API has a cooldown of 1800 seconds.
        /// </remarks>
        /// <param name="appid"> The Steam App ID to get special items for. Valid values are: 440, 570, 730.</param>
        /// <returns>The root object for the IGetSpecialItems API response.</returns>
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
