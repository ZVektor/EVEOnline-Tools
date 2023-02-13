using EVEOnline.Logic.ServicesAPI;
using static EVEOnline.MultiColoredModernUI;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using EVEOnline.Data.Models;
using EVEOnline.Logic.ServicesDB.Interfaces;
using EVEOnline.Logic.ServicesDB;
using EVEOnline.Logic.ModelsAPI;
using System.Windows.Forms;
using System.Linq;
using EVEOnline.Logic.ServicesAPI.Interfaces;

namespace EVEOnline.Forms
{
    public partial class FormTrade : Form
    {
        MyEveonlineDbContext dbContext = new MyEveonlineDbContext();
        HttpClient httpClient = new HttpClient();
        public FormTrade()
        {
            InitializeComponent();
        }

        private void FormTrade_Load(object sender, EventArgs e)
        {
            LoadTheme();
            //checkedListBox1
            dataGridView1.DataSource = dbContext.TbMarketOrders.Where(s => s.IsBuyOrder == false).OrderBy(s => s.Price).ToList();

            dataGridView2.DataSource = dbContext.TbMarketOrders.Where(s => s.IsBuyOrder == true).OrderByDescending(s => s.Price).ToList();
            //personData.OrderByDescending(s => s.FirstName),



        }
        #region Дизайн
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            //label4.ForeColor = ThemeColor.SecondaryColor;
            //label5.ForeColor = ThemeColor.PrimaryColor;
        }
        #endregion

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку ОРДЕРОВ");
            MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
            var orderList = await marketOrders.GetOrders(10000032, 1);
            if (orderList != null)
            {
                var ordersCount = orderList.Count;
                listBox1.Items.Add("Найдено ОРДЕРОВ: " + ordersCount.ToString());
                dataGridView1.DataSource = orderList;
                for (int i = 0; i < ordersCount; i++)
                {
                    var order = orderList[i];
                    IMarketOrderServiceDB marketOrder = new MarketOrderServiceDB(dbContext);
                    TbMarketOrder data = await marketOrder.GetMarketOrder(orderList[i].order_id);

                    if (data != null)
                    {
                        listBox1.Items.Add("ОРДЕР [" + orderList[i].order_id.ToString() + "] --- Есть в базе");
                    }
                    else
                    {
                        //ЗАТЫЧКА - ПОКА НЕ МОГУ РЕШИТЬ
                        var market_order_location_id = orderList[i].location_id;
                        if (market_order_location_id > 69999999)
                        {
                            market_order_location_id = 66666666;
                        }

                        IUniverseStationServiceDB universeStationServiceDB = new UniverseStationServiceDB(dbContext);
                        TbUniverseStation dataUniverseStation = await universeStationServiceDB.GetUniverseStation(market_order_location_id);
                        if (dataUniverseStation != null)
                        {
                            listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- Есть в базе");
                        }
                        else
                        {
                            listBox1.Items.Add("СТАНЦИИ [" + market_order_location_id.ToString() + "] --- Нет в базе. Добавляем .....");
                            //Делаем запрос по СТАНЦИИ через API чтоб узнать ТИП базы и добавить ТИП - БЕЗ добавления в саму базу
                            IUniverse universeStationModelAPI = new Universe(httpClient);


                            UniverseStationModel getUniverseStationAPI = await universeStationModelAPI.GetUniverseStationAsync(market_order_location_id, "en", false);
                            if (getUniverseStationAPI != null)
                            {
                                listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- Провряем есть ли ТИП [" + getUniverseStationAPI.type_id.ToString() + "] в базе");

                                var market_order_location_id_type_id = getUniverseStationAPI.type_id;
                                IUniverseTypeServiceDB universeTypeServiceDB = new UniverseTypeServiceDB(dbContext);
                                TbUniverseType dataUniverseType = await universeTypeServiceDB.GetUniverseType(market_order_location_id_type_id);

                                if (dataUniverseType != null)
                                {
                                    listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- ТИП [" + market_order_location_id_type_id.ToString() + "] --- Есть в базе");
                                }
                                else
                                {
                                    listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- ТИП [" + market_order_location_id_type_id.ToString() + "] --- Нет в базе. Добавляем .....");
                                    IUniverse universeTypeModelAPI = new Universe(httpClient);
                                    UniverseTypeModel getUniverseTypeAPI = await universeTypeModelAPI.GetUniverseTypeAsync(market_order_location_id_type_id, "en", true);
                                    if (getUniverseTypeAPI != null)
                                        listBox1.Items.Add("ТИП [" + market_order_location_id_type_id.ToString() + "--- Добавлен в базу!");
                                    else
                                        listBox1.Items.Add("ТИП [" + market_order_location_id_type_id.ToString() + "--- Не могу добавить в базу!");

                                }

                            }
                            else
                            {
                                listBox1.Items.Add("СТАНЦИИ [" + market_order_location_id.ToString() + "] --- НЕ СУЩЕСТВУЕТ в evetech.net");
                            }

                            getUniverseStationAPI = await universeStationModelAPI.GetUniverseStationAsync(market_order_location_id, "en", true);
                            if (getUniverseStationAPI != null)
                                listBox1.Items.Add("СТАНЦИИ [" + getUniverseStationAPI.station_id.ToString() + "--- Добавлен в базу!");
                            else
                                listBox1.Items.Add("СТАНЦИИ [" + getUniverseStationAPI.station_id.ToString() + "--- Не могу добавить в базу!");

                        }

                        TbMarketOrder newMarketOrder = new TbMarketOrder();
                        newMarketOrder.Id = orderList[i].order_id;
                        newMarketOrder.Duration = orderList[i].duration;
                        newMarketOrder.IsBuyOrder = orderList[i].is_buy_order;
                        newMarketOrder.Issued = orderList[i].issued;
                        //ЗАТЫЧКА - ПОКА НЕ МОГУ РЕШИТЬ
                        if (orderList[i].location_id > 69999999)
                        {
                            orderList[i].location_id = 66666666;
                        }
                        newMarketOrder.LocationId = orderList[i].location_id;//Занести в базу
                        newMarketOrder.MinVolume = orderList[i].min_volume;
                        newMarketOrder.Price = orderList[i].price;
                        newMarketOrder.Range = orderList[i].range;
                        newMarketOrder.SystemId = orderList[i].system_id;
                        newMarketOrder.TypeId = orderList[i].type_id; //Занести в базу
                        newMarketOrder.VolumeRemain = orderList[i].volume_remain;
                        newMarketOrder.VolumeTotal = orderList[i].volume_total;
                        var postMarketOrder = await marketOrder.PostMarketOrder(newMarketOrder);

                        if (postMarketOrder != null)
                            listBox1.Items.Add("ОРДЕР: [" + orderList[i].order_id + "] --- Добавлен в базу!");
                        else
                            listBox1.Items.Add("ОРДЕР: [" + orderList[i].order_id + "] --- Не могу добавить в базу !!!");
                    }

                }
            }
            else
            {
                listBox1.Items.Add("Нет ОРДЕРОВ");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Сколько РЕГИОНОВ в базе?");
            List<TbUniverseRegion> getRegion = new List<TbUniverseRegion>(); //Можно убрать через VAR

            IRegionService regionService = new RegionService(dbContext);
            getRegion = await regionService.GetRegions();
            int getRegionCoutn = getRegion.Count;
            listBox1.Items.Add("В базе - " + getRegionCoutn + " РЕГИОНОВ");
            List<MarketsOrdersModel> marketOrdersAdd = new List<MarketsOrdersModel>();
            for (int i = 0; i < getRegionCoutn; i++)
            {
                MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
                //List<MarketsOrdersModel> marketOrdersAdd = new List<MarketsOrdersModel>();
                var orderList = await marketOrders.GetOrders(getRegion[i].Id, 1);
                if (orderList != null)
                {
                    marketOrdersAdd.AddRange(orderList);
                    var ordersCount = orderList.Count;
                    listBox1.Items.Add("В Регионе:[" + getRegion[i].Name + "] найдено ОРДЕРОВ: " + ordersCount.ToString());
                    //dataGridView1.DataSource = marketOrdersAdd;
                }
                else
                {
                    listBox1.Items.Add("Нет ОРДЕРОВ");
                }
            }
            dataGridView1.DataSource = marketOrdersAdd;
            var marketOrdersAddCount = marketOrdersAdd.Count;
            listBox1.Items.Add("Всего ОРДЕРОВ" + marketOrdersAddCount);
            listBox1.Items.Add("Закончили упражнение!");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int ordersCount = 0;
            listBox1.Items.Add("Сколько РЕГИОНОВ в базе?");
            List<TbUniverseRegion> getRegion = new List<TbUniverseRegion>(); //Можно убрать через VAR

            IRegionService regionService = new RegionService(dbContext);
            getRegion = await regionService.GetRegions();
            int getRegionCoutn = getRegion.Count;
            listBox1.Items.Add("В базе - " + getRegionCoutn + " РЕГИОНОВ");
            List<MarketsOrdersModel> marketOrdersAdd = new List<MarketsOrdersModel>();
            for (int i = 0; i < getRegionCoutn; i++)
            {
                MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
                //List<MarketsOrdersModel> marketOrdersAdd = new List<MarketsOrdersModel>();
                var orderList = await marketOrders.GetOrders(getRegion[i].Id, 1);
                if (orderList != null)
                {
                    marketOrdersAdd.AddRange(orderList);
                    ordersCount = orderList.Count;
                    listBox1.Items.Add("В Регионе:[" + getRegion[i].Name + "] найдено ОРДЕРОВ: " + ordersCount.ToString());
                    //dataGridView1.DataSource = marketOrdersAdd;
                }
                else
                {
                    listBox1.Items.Add("Нет ОРДЕРОВ");
                }
            }
            dataGridView1.DataSource = marketOrdersAdd;
            var marketOrdersAddCount = marketOrdersAdd.Count;
            listBox1.Items.Add("Всего ОРДЕРОВ" + marketOrdersAddCount);
            listBox1.Items.Add("Закончили упражнение!");



            for (int i = 0; i < marketOrdersAddCount; i++)
            {
                var order = marketOrdersAdd[i];
                IMarketOrderServiceDB marketOrder = new MarketOrderServiceDB(dbContext);
                TbMarketOrder data = await marketOrder.GetMarketOrder(marketOrdersAdd[i].order_id);

                if (data != null)
                {
                    listBox1.Items.Add("ОРДЕР [" + marketOrdersAdd[i].order_id.ToString() + "] --- Есть в базе");
                }
                else
                {
                    //ЗАТЫЧКА - ПОКА НЕ МОГУ РЕШИТЬ
                    var market_order_location_id = marketOrdersAdd[i].location_id;
                    if (market_order_location_id > 69999999)
                    {
                        market_order_location_id = 66666666;
                    }

                    IUniverseStationServiceDB universeStationServiceDB = new UniverseStationServiceDB(dbContext);
                    TbUniverseStation dataUniverseStation = await universeStationServiceDB.GetUniverseStation(market_order_location_id);
                    if (dataUniverseStation != null)
                    {
                        listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- Есть в базе");
                    }
                    else
                    {
                        listBox1.Items.Add("СТАНЦИИ [" + market_order_location_id.ToString() + "] --- Нет в базе. Добавляем .....");
                        //Делаем запрос по СТАНЦИИ через API чтоб узнать ТИП базы и добавить ТИП - БЕЗ добавления в саму базу
                        IUniverse universeStationModelAPI = new Universe(httpClient);


                        UniverseStationModel getUniverseStationAPI = await universeStationModelAPI.GetUniverseStationAsync(market_order_location_id, "en", false);
                        if (getUniverseStationAPI != null)
                        {
                            listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- Провряем есть ли ТИП [" + getUniverseStationAPI.type_id.ToString() + "] в базе");

                            var market_order_location_id_type_id = getUniverseStationAPI.type_id;
                            IUniverseTypeServiceDB universeTypeServiceDB = new UniverseTypeServiceDB(dbContext);
                            TbUniverseType dataUniverseType = await universeTypeServiceDB.GetUniverseType(market_order_location_id_type_id);

                            if (dataUniverseType != null)
                            {
                                listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- ТИП [" + market_order_location_id_type_id.ToString() + "] --- Есть в базе");
                            }
                            else
                            {
                                listBox1.Items.Add("СТАНЦИЯ [" + market_order_location_id.ToString() + "] --- ТИП [" + market_order_location_id_type_id.ToString() + "] --- Нет в базе. Добавляем .....");
                                IUniverse universeTypeModelAPI = new Universe(httpClient);
                                UniverseTypeModel getUniverseTypeAPI = await universeTypeModelAPI.GetUniverseTypeAsync(market_order_location_id_type_id, "en", true);
                                if (getUniverseTypeAPI != null)
                                    listBox1.Items.Add("ТИП [" + market_order_location_id_type_id.ToString() + "--- Добавлен в базу!");
                                else
                                    listBox1.Items.Add("ТИП [" + market_order_location_id_type_id.ToString() + "--- Не могу добавить в базу!");

                            }

                        }
                        else
                        {
                            listBox1.Items.Add("СТАНЦИИ [" + market_order_location_id.ToString() + "] --- НЕ СУЩЕСТВУЕТ в evetech.net");
                        }

                        getUniverseStationAPI = await universeStationModelAPI.GetUniverseStationAsync(market_order_location_id, "en", true);
                        if (getUniverseStationAPI != null)
                            listBox1.Items.Add("СТАНЦИИ [" + getUniverseStationAPI.station_id.ToString() + "--- Добавлен в базу!");
                        else
                            listBox1.Items.Add("СТАНЦИИ [" + getUniverseStationAPI.station_id.ToString() + "--- Не могу добавить в базу!");

                    }

                    TbMarketOrder newMarketOrder = new TbMarketOrder();
                    newMarketOrder.Id = marketOrdersAdd[i].order_id;
                    newMarketOrder.Duration = marketOrdersAdd[i].duration;
                    newMarketOrder.IsBuyOrder = marketOrdersAdd[i].is_buy_order;
                    newMarketOrder.Issued = marketOrdersAdd[i].issued;
                    //ЗАТЫЧКА - ПОКА НЕ МОГУ РЕШИТЬ
                    if (marketOrdersAdd[i].location_id > 69999999)
                    {
                        marketOrdersAdd[i].location_id = 66666666;
                    }
                    newMarketOrder.LocationId = marketOrdersAdd[i].location_id;//Занести в базу
                    newMarketOrder.MinVolume = marketOrdersAdd[i].min_volume;
                    newMarketOrder.Price = marketOrdersAdd[i].price;
                    newMarketOrder.Range = marketOrdersAdd[i].range;
                    newMarketOrder.SystemId = marketOrdersAdd[i].system_id;
                    newMarketOrder.TypeId = marketOrdersAdd[i].type_id; //Занести в базу
                    newMarketOrder.VolumeRemain = marketOrdersAdd[i].volume_remain;
                    newMarketOrder.VolumeTotal = marketOrdersAdd[i].volume_total;
                    var postMarketOrder = await marketOrder.PostMarketOrder(newMarketOrder);

                    if (postMarketOrder != null)
                        listBox1.Items.Add("ОРДЕР: [" + marketOrdersAdd[i].order_id + "] --- Добавлен в базу!");
                    else
                        listBox1.Items.Add("ОРДЕР: [" + marketOrdersAdd[i].order_id + "] --- Не могу добавить в базу !!!");
                }

            }
        }

    }
}

