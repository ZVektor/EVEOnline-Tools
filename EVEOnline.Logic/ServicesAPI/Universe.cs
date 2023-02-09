using EVEOnline.Logic.ModelsAPI;
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

        //TODO Сделать обработчик на прерывание и ошибки соединения
        /// <summary>
        /// Получает значения методов Universe
        /// </summary>
        /// <param name="argUniverse">Методы Universe</param>
        /// <returns>Массив чисел - int[]</returns>
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
        /// <param name="id">Id РЕГИОНА</param>
        /// <param name="language">На каком языке</param>
        /// <returns>Модель РЕГИОНА</returns>
        public async Task<URegion> GetRegionAsync(int id, string? language)
        {
            string fullHost = _hostRegions + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<URegion>(fullHost);
            return response;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Получить информацию СОЗВЕЗДИЯ
        /// </summary>
        /// <param name="id">Id СОЗВЕЗДИЯ</param>
        /// <param name="language">На каком языке</param>
        /// <returns>Модель СОЗВЕЗДИЯ</returns>
        public async Task<UConstellation> GetConstellationAsync(int id, string? language)
        {
            string fullHost = _hostConstellations + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<UConstellation>(fullHost);
            return response;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Получить информацию СОЛНЕЧНОЙ СИСТЕМЫ
        /// </summary>
        /// <param name="id">Id СОЛНЕЧНОЙ СИСТЕМЫ</param>
        /// <param name="language">На каком языке</param>
        /// <returns>Модель СОЛНЕЧНОЙ СИСТЕМЫ</returns>
        public async Task<USystem> GetSystemAsync(int id, string? language)
        {
            string fullHost = _hostSystems + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<USystem>(fullHost);
            return response;
            //throw new NotImplementedException();
        }
    }
}
