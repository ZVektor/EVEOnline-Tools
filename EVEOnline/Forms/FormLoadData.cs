using EVEOnline.Logic.ServicesAPI.Interfaces;
using EVEOnline.Logic.ServicesAPI;
using EVEOnline.Logic.ServicesDB.Interfaces;
using EVEOnline.Logic.ServicesDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEOnline.Data.Models;
using static EVEOnline.MultiColoredModernUI;

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
                    Uregion data = await regionService.GetRegion(regionList[i]);

                    if (data != null)
                    {
                        listBox1.Items.Add("Регион: " + regionList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var region = await universe.GetRegionAsync(regionList[i], "ru");
                        Uregion newRegion = new Uregion();
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
                    Uconstellation data = await constellationService.GetConstellation(constellationsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("СОЗВЕЗДИЕ: " + constellationsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var constellation = await universe.GetConstellationAsync(constellationsList[i], "ru");
                        Uconstellation newConstellation = new Uconstellation();
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
                    Usystem data = await systemService.GetSystem(systemsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("СОЛНЕЧНАЯ СИСТЕМА: " + systemsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var system = await universe.GetSystemAsync(systemsList[i], "ru");
                        Usystem newSystem = new Usystem();
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
    }
}
