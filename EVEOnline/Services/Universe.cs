using Azure;
using EVEOnline.Data.Models;
using EVEOnline.Logic.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EVEOnline.Services
{
    public static class Universe //: IUniverse
    {
        //private static readonly HttpClient _httpClient;
        private static readonly string _host = "https://esi.evetech.net/dev/universe/";
        private static readonly string _hostRegions = "https://esi.evetech.net/dev/universe/regions/";
        private static readonly string _hostConstellations = "https://esi.evetech.net/dev/universe/constellations/";
        private static readonly string _hostSystems = "https://esi.evetech.net/dev/universe/systems/";

        private static readonly string _datasource = "?datasource=tranquility";

        //private static readonly string _fullHost ="";
        //public static Universe(HttpClient httpClient) => _httpClient = httpClient;

        /// <summary>
        /// Get regions
        /// </summary>
        /// <returns></returns>
        public static async Task<int[]> GetUniverse(string arg)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                try
                {
                    string _fullHost = _host + arg + "/" + _datasource;
                    HttpResponseMessage response = await _httpClient.GetAsync(_fullHost);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadFromJsonAsync<int[]>();
                    return result;
                }
                catch (HttpRequestException e)
                {

                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return null;
        }


        /// <summary>
        /// Get region information
        /// </summary>
        /// <param name="id">Номер региона</param>
        /// <returns></returns>
        public static async Task<URegion> GetRegion(int id, string language)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                string fullHost = _hostRegions + id.ToString()+"/" + _datasource + "&language=" + language;
                var response = await _httpClient.GetFromJsonAsync<URegion>(fullHost);
                return response;
            }
        }
        public static async Task<UConstellation> GetConstellation(int id, string language)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                string fullHost = _hostConstellations + id.ToString() + "/" + _datasource + "&language=" + language;
                var response = await _httpClient.GetFromJsonAsync<UConstellation>(fullHost);
                return response;
            }
        }
        public static async Task<USystem> GetSystem(int id, string language)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                string fullHost = _hostSystems + id.ToString() + "/" + _datasource + "&language=" + language;
                var response = await _httpClient.GetFromJsonAsync<USystem>(fullHost);
                return response;
            }
        }


    }


}
