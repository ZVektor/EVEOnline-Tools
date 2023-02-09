using EVEOnline.Logic.Models;
using EVEOnline.Logic.ServicesAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesAPI
{
    public class Universe : IUniverse
    {
        private readonly HttpClient _httpClient;
        public Universe(HttpClient httpClient) => _httpClient = httpClient;

        private static readonly string _host = "https://esi.evetech.net/dev/universe/";
        private static readonly string _hostRegions = "https://esi.evetech.net/dev/universe/regions/";
        private static readonly string _hostConstellations = "https://esi.evetech.net/dev/universe/constellations/";
        private static readonly string _hostSystems = "https://esi.evetech.net/dev/universe/systems/";

        private static readonly string _datasource = "?datasource=tranquility";

        public async Task<int[]> GetUniverseAsync(string argUniverse)
        {
            try
            {
                string _fullHost = _host + argUniverse + "/" + _datasource;
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
            return null;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Получить информацию РЕГИОНА
        /// </summary>
        /// <param name="id">Id Региона</param>
        /// <param name="language">На каком языке</param>
        /// <returns></returns>
        public async Task<URegion> GetRegionAsync(int id, string? language)
        {
            string fullHost = _hostRegions + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<URegion>(fullHost);
            return response;

            //throw new NotImplementedException();
        }
        public Task<UConstellation> GetConstellationAsync(int id, string? language)
        {
            throw new NotImplementedException();
        }
        public Task<USystem> GetSystemAsync(int id, string? language)
        {
            throw new NotImplementedException();
        }
    }
}
