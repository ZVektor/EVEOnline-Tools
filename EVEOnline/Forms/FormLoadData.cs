using EVEOnline.Logic.ServicesAPI.Interfaces;
using EVEOnline.Logic.ServicesAPI;
using EVEOnline.Logic.ServicesDB.Interfaces;
using EVEOnline.Logic.ServicesDB;
using EVEOnline.Data.Models;
using static EVEOnline.MultiColoredModernUI;
using EVEOnline.Logic.ModelsAPI;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace EVEOnline.Forms
{
    public partial class FormLoadData : Form
    {
        //private readonly IUniverse _universe;
        //private readonly IRegionService _regionService;
        MyEveonlineDbContext DbContext_dbContext = new MyEveonlineDbContext();
        HttpClient httpClient = new HttpClient();
        public FormLoadData()
        {
            InitializeComponent();
        }

        #region Дизайнерский UI
        private void FormLoadData_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
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

        private async void btnAddRegions_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку Регионов");
            IUniverse universe = new Universe(httpClient);//Надо как то переделать чтоб не вызывать каждый раз!
            int[] regionList = await universe.GetUniverseAsync("regions");

            if (regionList != null)
            {
                var regionCount = regionList.Length;
                progressBar1.Maximum = regionCount; //Максимальное значение
                listBox1.Items.Add("Найдено регионов: " + regionCount.ToString());

                for (int i = 0; i < regionCount; i++)
                {
                    IRegionService regionService = new RegionService(DbContext_dbContext); //Надо как то переделать чтоб не вызывать каждый раз!
                    TbUniverseRegion data = await regionService.GetRegion(regionList[i]);

                    if (data != null)
                    {
                        listBox1.Items.Add("Регион: " + regionList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var region = await universe.GetRegionAsync(regionList[i], "ru");
                        TbUniverseRegion newRegion = new TbUniverseRegion();
                        newRegion.Id = regionList[i];
                        newRegion.Name = region.name;
                        newRegion.Description = region.description;
                        var postRegion = await regionService.PostRegion(newRegion);
                        if (postRegion != null)
                            listBox1.Items.Add("Регион: [" + regionList[i] + ":" + region.name + "] --- Добавлен в базу!");
                        else
                            listBox1.Items.Add("Регион: [" + regionList[i] + ":" + region.name + "] --- Не могу добавить в базу !!!");
                    }
                    progressBar1.PerformStep(); //вызываем этот метод для увеличения шкалы progressBar
                }
                listBox1.Items.Add("Все регионы загружены в базу" + regionCount.ToString());
            }
            else
            {
                listBox1.Items.Add("Нет регионов");
            }
        }

        private async void btnAddConstellations_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку Созвездий");
            IUniverse universe = new Universe(httpClient);
            var constellationsList = await universe.GetUniverseAsync("constellations");

            if (constellationsList != null)
            {
                var constellationsCount = constellationsList.Length;
                progressBar2.Maximum = constellationsCount; //по умолчанию
                listBox1.Items.Add("Найдено СОЗВЕЗДИЙ: " + constellationsCount.ToString());

                for (int i = 0; i < constellationsCount; i++)
                {
                    IConstellationService constellationService = new СonstellationService(DbContext_dbContext); //Надо как то переделать чтоб не вызывать каждый раз!
                    TbUniverseConstellation data = await constellationService.GetConstellation(constellationsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("СОЗВЕЗДИЕ: " + constellationsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var constellation = await universe.GetConstellationAsync(constellationsList[i], "ru");
                        TbUniverseConstellation newConstellation = new TbUniverseConstellation();
                        newConstellation.Id = constellationsList[i];
                        newConstellation.Name = constellation.name;
                        newConstellation.PositionX = constellation.position.x;
                        newConstellation.PositionY = constellation.position.y;
                        newConstellation.PositionZ = constellation.position.z;
                        //newConstellation.PositionZ = (long)constellation.position.z;
                        newConstellation.RegionId = constellation.region_id;

                        var postConstellation = await constellationService.PostConstellation(newConstellation);
                        if (postConstellation != null)
                            listBox1.Items.Add("СОЗВЕЗДИЕ: [" + constellationsList[i] + ":" + constellation.name + "] --- Добавлен в базу!");
                        else
                            listBox1.Items.Add("СОЗВЕЗДИЕ: [" + constellationsList[i] + ":" + constellation.name + "] --- Не могу добавить в базу !!!");
                    }




                    progressBar2.PerformStep(); //вызываем этот метод для увеличения шкалы progressBar
                }
                listBox1.Items.Add("СОЗВЕЗДИЯ загружены" + constellationsCount.ToString());
            }
            else
            {
                listBox1.Items.Add("Нет СОЗВЕЗДИЙ");
            }
        }

        private async void btmAddSystems_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку СОЛНЕЧНЫХ СИСТЕМ");
            IUniverse universe = new Universe(httpClient);
            var systemsList = await universe.GetUniverseAsync("systems");

            if (systemsList != null)
            {
                var systemsCount = systemsList.Length;
                progressBar3.Maximum = systemsCount; //по умолчанию
                listBox1.Items.Add("Найдено СОЛНЕЧНЫХ СИСТЕМ: " + systemsCount.ToString());

                for (int i = 0; i < systemsCount; i++)
                {
                    ISystemService systemService = new SystemService(DbContext_dbContext); //Надо как то переделать чтоб не вызывать каждый раз!
                    TbUniverseSystem data = await systemService.GetSystem(systemsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("СОЛНЕЧНАЯ СИСТЕМА: " + systemsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var system = await universe.GetSystemAsync(systemsList[i], "ru");
                        TbUniverseSystem newSystem = new TbUniverseSystem();
                        newSystem.Id = systemsList[i];
                        newSystem.Name = system.name;
                        newSystem.PositionX = system.position.x;
                        newSystem.PositionY = system.position.y;
                        newSystem.PositionZ = system.position.z;
                        newSystem.SecurityClass = system.security_class;
                        newSystem.SecurityStatus = system.security_status;
                        newSystem.StarId = system.star_id;
                        newSystem.ConstellationId = system.constellation_id;

                        var postSystem = await systemService.PostSystem(newSystem);
                        if (postSystem != null)
                            listBox1.Items.Add("СОЗВЕЗДИЕ: [" + systemsList[i] + ":" + system.name + "] --- Добавлен в базу!");
                        else
                            listBox1.Items.Add("СОЗВЕЗДИЕ: [" + systemsList[i] + ":" + system.name + "] --- Не могу добавить в базу !!!");
                    }
                    progressBar3.PerformStep();
                }
                listBox1.Items.Add("Созвездия загружены" + systemsCount.ToString());
            }
            else
            {
                listBox1.Items.Add("Нет созвездий");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1 == null) return;
            else
            {
                listBox1.Items.Add("Проверяем есть ли ТИП в базе");
                var typsId = Convert.ToInt32(textBox1.Text);
                IUniverseTypeServiceDB universeTypeServiceDB = new UniverseTypeServiceDB(DbContext_dbContext); //Надо как то переделать чтоб не вызывать каждый раз!
                var data = await universeTypeServiceDB.GetUniverseType(typsId);
                if (data != null)
                {
                    listBox1.Items.Add("В базе есть этот ТИП");
                }
                else
                {

                    listBox1.Items.Add("Загружаем ТИП [" + typsId + "] по средствам API");

                    IUniverse universe = new Universe(httpClient);
                    var getUniverseType = await universe.GetUniverseTypeAsync(typsId, "en", true);
                    if (getUniverseType != null)
                    {
                        listBox1.Items.Add("ТИП [" + getUniverseType.type_id + "-" + getUniverseType.name + "] --- Загружен");
                        var postdata = await universeTypeServiceDB.GetUniverseType(typsId);
                        if (postdata != null)
                            listBox1.Items.Add("ТИП [" + getUniverseType.type_id + "-" + getUniverseType.name + "] --- Добавлен в базу!");
                        else
                            listBox1.Items.Add("ТИП [" + getUniverseType.type_id + "-" + getUniverseType.name + "] --- Не могу добавить в базу !!!");
                    }
                    else
                        listBox1.Items.Add("ТИП [" + typsId + "] --- Нет на сервере !!!");

                }

            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
            IUniverseTypeBSServiceDB universeTypeBSServiceDB = new UniverseTypeBSServiceDB(DbContext_dbContext);
            IUniverse universeTypeBSModelAPI = new Universe(httpClient);
            if (textBox1 == null) return;
            else
            {
                var regionId = Convert.ToInt32(textBox2.Text);
                listBox1.Items.Add("Начинаем загрузку ОРДЕРОВ");
                bool arrLoop = true;
                int arrI = 1;

                while (arrLoop)
                {
                    //MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
                    var orderList = await marketOrders.GetOrders(regionId, arrI);
                    if (orderList != null)
                    {
                        int orderListCount = orderList.Count;
                        listBox1.ForeColor = Color.Red;
                        listBox1.Items.Add("СТРАНИЦА №: - " + arrI + " - Найдено ОРДЕРОВ: - " + orderListCount);
                        listBox1.ForeColor = Color.Black;
                        for (int i = 0; i < orderListCount; i++)
                        {

                            var market_order_type_id = orderList[i].type_id;

                            //IUniverseTypeBSServiceDB universeTypeBSServiceDB = new UniverseTypeBSServiceDB(DbContext_dbContext);
                            TbUniverseTypeB dataUniverseTypeBS = await universeTypeBSServiceDB.GetUniverseTypeBS(market_order_type_id);

                            if (dataUniverseTypeBS != null)
                            {
                                //listBox1.Items.Add("ТИП [" + dataUniverseTypeBS.Id + " - " + dataUniverseTypeBS.Name + "] --- Есть в базе");
                            }
                            else
                            {
                                //listBox1.Items.Add("ТИП [" + market_order_type_id + "] --- НЕТ в базе! --- ДОБАВЛЯЕМ");
                                //IUniverse universeTypeBSModelAPI = new Universe(httpClient);
                                UniverseTypeModel getUniverseTypeAPI = await universeTypeBSModelAPI.GetUniverseTypeBSAsync(market_order_type_id, "en", true);
                                if (getUniverseTypeAPI != null)
                                    listBox1.Items.Add("ТИП [" + getUniverseTypeAPI.type_id + " - " + getUniverseTypeAPI.name + "] --- Добавлен в базу!");
                                else
                                    listBox1.Items.Add("ТИП [" + market_order_type_id + "]--- Не могу добавить в базу!");

                            }




                        }
                        arrI++;
                    }
                    else
                    {
                        arrLoop = false;
                        listBox1.Items.Add("Обработано СТРАНИЦ: - " + Convert.ToString(arrI - 1));
                    }
                }
            }

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            MarketOrderServiseAPI marketOrders = new MarketOrderServiseAPI(httpClient);//Надо как то переделать чтоб не создавать каждый раз!
            IUniverseTypeBSServiceDB universeTypeBSServiceDB = new UniverseTypeBSServiceDB(DbContext_dbContext);
            IUniverse universeTypeBSModelAPI = new Universe(httpClient);
            var regionId = Convert.ToInt32(textBox2.Text);

            var getUniverseTypes = await universeTypeBSServiceDB.GetUniverseTypesBS();
            int getUniverseTypesCont = getUniverseTypes.Count;
            progressBar4.Maximum = getUniverseTypesCont;
            for (int i = 0; i < getUniverseTypesCont; i++)
            {
                var currentTypeId = getUniverseTypes[i].Id;
                var orderList = await marketOrders.GetOrders(regionId, 1, currentTypeId);
                if (orderList != null)
                {
                    //listBox1.Items.Add("ТИП: [" + getUniverseTypes[i].Id + " - " + getUniverseTypes[i].Name + "] Найдено ОРДЕРОВ --- " + orderList.Count);

                    decimal tempOrderListSellPrice = 0;
                    decimal tempOrderListBuyPrice = 0;

                    var orderListSell = orderList.Where(s => s.is_buy_order == false).OrderBy(s => s.price).FirstOrDefault();
                    if (orderListSell != null)
                    {
                        tempOrderListSellPrice = orderListSell.price * orderListSell.volume_remain;

                    }

                    var orderListBuy = orderList.Where(s => s.is_buy_order == true).OrderByDescending(s => s.price).FirstOrDefault();
                    if (orderListBuy != null)
                    {
                        tempOrderListBuyPrice = orderListBuy.price * orderListBuy.volume_remain;
                    }

                    decimal orderListMorja = 0;
                    if ((orderListSell != null) && (orderListBuy != null))
                    {
                        orderListMorja = (orderListBuy.price / orderListSell.price) - 1;
                    }
                    if ((double)orderListMorja > 0.1) 
                    {
                        listBox1.Items.Add("ТИП: [" + getUniverseTypes[i].Id + " - " + getUniverseTypes[i].Name + "] Найдено ОРДЕРОВ --- " + orderList.Count);
                        listBox1.Items.Add("MIN Цена продажи  --- " + orderListSell.price + " На общую сумму: " + tempOrderListSellPrice);
                        listBox1.Items.Add("MAX Цена покупки  --- " + orderListBuy.price + " На общую сумму: " + tempOrderListBuyPrice);
                        listBox1.Items.Add("МОРЖА СОСТАВЛЯЕТ: " + orderListMorja);
                        listBox1.Items.Add("");
                    }
                }
                else
                {
                    listBox1.Items.Add("ТИП: [" + getUniverseTypes[i].Id + " - " + getUniverseTypes[i].Name + "] Не найдено ОРДЕРОВ");
                }
                progressBar4.PerformStep();
            }
            listBox1.Items.Add("ЗАКОНЧИЛИ!!!");
        }
    }
}
