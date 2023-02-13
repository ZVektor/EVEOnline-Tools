using EVEOnline.Logic.ModelsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesAPI
{
    public class MarketOrderServiseAPI
    {
        private readonly HttpClient _httpClient;
        public MarketOrderServiseAPI(HttpClient httpClient) => _httpClient = httpClient;


        //https://esi.evetech.net/dev/markets/10000032/orders/?datasource=tranquility&order_type=all&page=1&type_id=37
        private static readonly string _host = "https://esi.evetech.net/dev/markets/";
        //private static readonly string _hostRegions = "https://esi.evetech.net/dev/universe/regions/";
        //private static readonly string _hostConstellations = "https://esi.evetech.net/dev/universe/constellations/";
        //private static readonly string _hostSystems = "https://esi.evetech.net/dev/universe/systems/";

        private static readonly string _datasource = "?datasource=tranquility";

        public async Task<List<MarketsOrdersModel>> GetOrders(int argRegionId, int argPage)
        {
            try
            {
                //string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage+ "&type_id=649"; //1230 Veldspar
                string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage ; //1230 Veldspar

                var arrOrder = await _httpClient.GetFromJsonAsync<MarketsOrdersModel[]>(_fullHost);
                var arrOrderCount = arrOrder.Count();
                List<MarketsOrdersModel> allOrders= new List<MarketsOrdersModel>();
                for (int i = 0; i < arrOrderCount; i++)
                {
                    allOrders.Add(arrOrder[i]);
                }




                return allOrders;
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return null;
            //throw new NotImplementedException();
        }
        public async Task<List<MarketsOrdersModel>> GetOrders(int argRegionId, int argPage, int argType)
        {
            try
            {
                string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage + "&type_id="+ argType; //1230 Veldspar

                //string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage + "&type_id=25591";                                                                                                             
                //string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage + "&type_id=37";                                                                                                           
                //string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage + "&type_id=44992"; //PLEX                                                                                                              
                //string _fullHost = _host + argRegionId + "/orders/" + _datasource + "&page=" + argPage + "&type_id=36"; //36 Mexalon


                var arrOrder = await _httpClient.GetFromJsonAsync<MarketsOrdersModel[]>(_fullHost);
                var arrOrderCount = arrOrder.Count();
                List<MarketsOrdersModel> allOrders = new List<MarketsOrdersModel>();
                for (int i = 0; i < arrOrderCount; i++)
                {
                    allOrders.Add(arrOrder[i]);
                }
                return allOrders;
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
