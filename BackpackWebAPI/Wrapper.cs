using System;
using System.Threading.Tasks;
using System.IO;
using BackpackWebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using BackpackWebAPI.Exceptions;
using BackpackWebAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BackpackWebAPI
{
    public class Wrapper : IWrapper
    {
        private string apiKey;
        public Wrapper(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<UserInfoRoot> GetUserInfoAsync(List<ulong> steamIds)
        {
            steamIds = steamIds.Take(100).ToList();
            string response = string.Empty;
            UserInfoRoot root = null;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/users/info/v1?steamids={string.Join(',', steamIds)}&key={apiKey}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<UserInfoRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetUserInfo API - check inputs and try again.", ex);
                }
            }

            return root;
        }

        public async Task<CommunityPricesRoot> GetCommunityPricesAsync(long since = 0)
        {
            string response = string.Empty;
            string sinceFormat = (since > 0) ? string.Empty : $"&since={since}";
            CommunityPricesRoot root = null;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/IGetPrices/v4?key={apiKey}{sinceFormat}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<CommunityPricesRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetCommunityPrices API - check inputs and try again.", ex);
                }
            }

            return root;
        }

        public async Task<CurrenciesRoot> GetCurrenciesAsync(int getRaw = 0)
        {
            string response = string.Empty;
            string rawFormat = (getRaw > 0 && getRaw < 3) ? string.Empty : $"&raw={getRaw}";
            CurrenciesRoot root = null;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/IGetCurrencies/v1?key={apiKey}{rawFormat}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<CurrenciesRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetCurrencies API - check inputs and try again.", ex);
                }
            }

            return root;
        }

        public async Task<PriceHistoryRoot> GetPriceHistoryAsync(string item, string quality, int appId = 440, bool craftable = true, int priceindex = 0)
        {
            string response = string.Empty;
            PriceHistoryRoot root = null;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/IGetPriceHistory/v1?item={item}&quality={quality}&key={apiKey}&appid={appId}&craftable={((craftable) ? 1 : 0)}&priceindex={priceindex}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<PriceHistoryRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetCommunityPrices API - check inputs and try again.", ex);
                }
                
            }

            return root;
        }

        public async Task<ClassifiedsSearchRoot> GetClassifiedsSearchAsync(string item, bool getItemNames = false, Dictionary<string, string> filters = null, string intent = "dual", int page = 1, int pageSize = 10, bool fold = true, ulong steamId = 0)
        {
            string response = string.Empty;
            ClassifiedsSearchRoot root = null;

            string filtersFormat = string.Empty;
            if (filters != null)
            {
                foreach (string s in filters.Keys)
                {
                    filtersFormat += $"&{s}={filters[s]}";
                }
            }

            string steamIdFormat = (steamId > 0) ? $"&steamid={steamId}" : string.Empty;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/classifieds/search/v1?item={item}&item_names={(getItemNames ? 1 : 0)}{filtersFormat}&intent={intent}&page={page}&page_size={pageSize}&fold={(fold ? 1 : 0)}{steamIdFormat}&key={this.apiKey}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<ClassifiedsSearchRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetClassifiedsListings API - check inputs and try again.", ex);
                }
            }

            return root;
        }

        public async Task<SpecialItemsRoot> GetSpecialItemsAsync(int appid = 440)
        {
            string response = string.Empty;
            SpecialItemsRoot root = null;

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    response = await http.GetStringAsync($"https://backpack.tf/api/IGetSpecialItems/v1?key={this.apiKey}&appid={appid}").ConfigureAwait(false);
                    root = JsonConvert.DeserializeObject<SpecialItemsRoot>(response);
                }
                catch (Exception ex)
                {
                    throw new BackpackRequestException("Error in GetSpecialItems API - check inputs and try again.", ex);
                }
            }

            return root;
        }
    }
}
