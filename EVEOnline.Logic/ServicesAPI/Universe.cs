using EVEOnline.Data.Models;
using EVEOnline.Logic.ModelsAPI;
using EVEOnline.Logic.ServicesAPI.Interfaces;
using EVEOnline.Logic.ServicesDB.Interfaces;
using EVEOnline.Logic.ServicesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace EVEOnline.Logic.ServicesAPI
{
    public class Universe : IUniverse
    {
        private readonly HttpClient _httpClient;
        private readonly MyEveonlineDbContext dbContext = new MyEveonlineDbContext();
        public Universe(HttpClient httpClient) => _httpClient = httpClient;

        private static readonly string _host = "https://esi.evetech.net/dev/universe/";
        private static readonly string _hostRegions = "https://esi.evetech.net/dev/universe/regions/";
        private static readonly string _hostConstellations = "https://esi.evetech.net/dev/universe/constellations/";
        private static readonly string _hostSystems = "https://esi.evetech.net/dev/universe/systems/";
        private static readonly string _hostTypes = "https://esi.evetech.net/dev/universe/types/";
        private static readonly string _hostStations = "https://esi.evetech.net/dev/universe/stations/";

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

        public async Task<UniverseTypeModel> GetUniverseTypeAsync(int id, string? language, bool addBD)
        {
            string fullHost = _hostTypes + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<UniverseTypeModel>(fullHost);
            if (addBD)
            {
                IUniverseTypeServiceDB universeTypeServiceDB = new UniverseTypeServiceDB(dbContext);
                TbUniverseType dataUniverseType = await universeTypeServiceDB.GetUniverseType(response.type_id);
                if (dataUniverseType == null)
                {
                    TbUniverseType newUniverseType = new TbUniverseType();
                    newUniverseType.Id = response.type_id;
                    newUniverseType.Name = response.name;
                    newUniverseType.Capacity = response.capacity;
                    newUniverseType.Description = response.description;

                    if (response.dogma_attributes != null)
                    {
                        var dogma_attributes = response.dogma_attributes.Length;
                        for (int i = 0; i < dogma_attributes; i++)
                        {
                            newUniverseType.DogmaAttributes += response.dogma_attributes[i].attribute_id + ",";
                        }
                    }
                    else
                    {
                        newUniverseType.DogmaAttributes = "";
                    }

                    if (response.dogma_effects != null)
                    {
                        var dogma_effects = response.dogma_effects.Length;

                        for (int i = 0; i < dogma_effects; i++)
                        {
                            newUniverseType.DogmaEffects += response.dogma_effects[i].effect_id + ",";
                        }
                    }
                    else
                    {
                        newUniverseType.DogmaEffects = "";
                    }

                    newUniverseType.GraphicId = response.graphic_id;
                    newUniverseType.GroupId = response.group_id;
                    newUniverseType.IconId = response.icon_id;
                    newUniverseType.MarketGroupId = response.market_group_id;
                    newUniverseType.Mass = response.mass;
                    newUniverseType.PackagedVolume = response.packaged_volume;
                    newUniverseType.PortionSize = response.portion_size;
                    newUniverseType.Published = response.published;
                    newUniverseType.Radius = response.radius;
                    newUniverseType.Volume = response.volume;

                    var postUniverseType = await universeTypeServiceDB.PostUniverseType(newUniverseType);
                }
            }

            return response;
            //throw new NotImplementedException();
        }

        public async Task<UniverseTypeModel> GetUniverseTypeBSAsync(int id, string? language, bool addBD)
        {
            string fullHost = _hostTypes + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<UniverseTypeModel>(fullHost);
            if (addBD)
            {
                IUniverseTypeBSServiceDB universeTypeBSServiceDB = new UniverseTypeBSServiceDB(dbContext);

                TbUniverseTypeB newUniverseTypeBS = new TbUniverseTypeB();
                newUniverseTypeBS.Id = response.type_id;
                newUniverseTypeBS.Name = response.name;
                newUniverseTypeBS.Capacity = response.capacity;
                newUniverseTypeBS.Description = response.description;
                //ЗАТЫЧКА
                newUniverseTypeBS.DogmaAttributes = "";
                newUniverseTypeBS.DogmaEffects = "";
                //ЗАТЫЧКА
                //if (response.dogma_attributes != null)
                //{
                //    var dogma_attributes = response.dogma_attributes.Length;
                //    for (int i = 0; i < dogma_attributes; i++)
                //    {
                //        newUniverseTypeBS.DogmaAttributes += response.dogma_attributes[i].attribute_id + ",";
                //    }
                //}
                //else
                //{
                //    newUniverseTypeBS.DogmaAttributes = "";
                //}

                //if (response.dogma_effects != null)
                //{
                //    var dogma_effects = response.dogma_effects.Length;

                //    for (int i = 0; i < dogma_effects; i++)
                //    {
                //        newUniverseTypeBS.DogmaEffects += response.dogma_effects[i].effect_id + ",";
                //    }
                //}
                //else
                //{
                //    newUniverseTypeBS.DogmaEffects = "";
                //}

                newUniverseTypeBS.GraphicId = response.graphic_id;
                newUniverseTypeBS.GroupId = response.group_id;
                newUniverseTypeBS.IconId = response.icon_id;
                newUniverseTypeBS.MarketGroupId = response.market_group_id;
                newUniverseTypeBS.Mass = response.mass;
                newUniverseTypeBS.PackagedVolume = response.packaged_volume;
                newUniverseTypeBS.PortionSize = response.portion_size;
                newUniverseTypeBS.Published = response.published;
                newUniverseTypeBS.Radius = response.radius;
                newUniverseTypeBS.Volume = response.volume;

                var postUniverseType = await universeTypeBSServiceDB.PostUniverseTypeBS(newUniverseTypeBS);
            }
            return response;
            //throw new NotImplementedException();
        }
        public async Task<UniverseStationModel> GetUniverseStationAsync(long id, string? language, bool addBD)
        {
            string fullHost = _hostStations + id.ToString() + "/" + _datasource + "&language=" + language;
            var response = await _httpClient.GetFromJsonAsync<UniverseStationModel>(fullHost);

            if (addBD)
            {
                IUniverseStationServiceDB universeStationServiceDB = new UniverseStationServiceDB(dbContext);
                TbUniverseStation dataUniverseStation = await universeStationServiceDB.GetUniverseStation(response.station_id);
                if (dataUniverseStation == null)
                {
                    TbUniverseStation newUniverseStation = new TbUniverseStation();
                    newUniverseStation.Id = response.station_id;
                    newUniverseStation.Name = response.name;
                    //TODO переделать переменную на float
                    newUniverseStation.MaxDockableShipVolume = (int)response.max_dockable_ship_volume;
                    newUniverseStation.OfficeRentalCost = (int)response.office_rental_cost;
                    newUniverseStation.Owner = response.owner;
                    newUniverseStation.PositionX = response.position.x;
                    newUniverseStation.PositionY = response.position.y;
                    newUniverseStation.PositionZ = response.position.z;
                    newUniverseStation.RaceId = response.race_id;
                    newUniverseStation.ReprocessingEfficiency = response.reprocessing_efficiency;
                    newUniverseStation.ReprocessingStationsTake = response.reprocessing_stations_take;
                    var services = response.services.Length;
                    for (int i = 0; i < services; i++)
                    {
                        newUniverseStation.Services += response.services[i];
                    }
                    newUniverseStation.SystemId = response.system_id;
                    newUniverseStation.TypeId = response.type_id;
                    var postUniverseStation = await universeStationServiceDB.PostUniverseStation(newUniverseStation);


                }
            }
            return response;
        }
    }
}
