using EVEOnline.Services;
using EVEOnline.Data.Models;
using System;
using Azure;
using EVEOnline.Logic.Models;
using EVEOnline.Logic.Services;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace EVEOnline
{
    public partial class Form1 : Form
    {
        //private IUniverse universe;
        //private readonly IRegionService _regionService;
        public IRegionService _regionService;
        //public IEnumerable<UniverseRegion> regionList = null;
        MyEveonlineDbContext DbContext_dbContext = new MyEveonlineDbContext();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAddRegion_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку Регионов");
            int[] regionList = null;
            regionList = await Universe.GetUniverse("regions");


            if (regionList != null)
            {
                var regionCount = regionList.Length;

                progressBar1.Minimum = 0; // по умолчанию
                progressBar1.Maximum = regionCount; //по умолчанию
                progressBar1.Step = 1; //по умолчанию

                listBox1.Items.Add("Найдено регионов: " + regionCount.ToString());

                for (int i = 0; i < regionList.Length; i++)
                {
                    Uregion data = new Uregion();

                    IRegionService regionService = new RegionService(DbContext_dbContext);
                    //var searchData = DbContext_dbContext.Uregions.FirstOrDefaultAsync(x => x.Id == regionList[i]);
                    //var searchData = await DbContext_dbContext.Uregions.FindAsync(1);
                    data = await regionService.GetRegion(regionList[i]);

                    if (data != null)
                    {
                        listBox1.Items.Add("Регион: "+ regionList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- Есть в базе");
                    }
                    else
                    {
                        var region = await Universe.GetRegion(regionList[i], "ru");
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
                    //listBox1.Items.Add(regionList[i].ToString()+" "+region.name + ": Ok");
                    //listBox1.Items.Add(region.description);
                    //listBox1.Items.Add("");
                    progressBar1.PerformStep(); //вызываем этот метод для увеличения шкалы progressBar
                }
                listBox1.Items.Add("Все регионы загружены в базу" + regionCount.ToString());
            }
            else
            {
                listBox1.Items.Add("Нет регионов");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку Созвездий");
            var constellationsList = await Universe.GetUniverse("constellations");


            if (constellationsList != null)
            {
                var constellationsCount = constellationsList.Length;
                progressBar2.Minimum = 0; // по умолчанию
                progressBar2.Maximum = constellationsCount; //по умолчанию
                progressBar2.Step = 1; //по умолчанию
                listBox1.Items.Add("Найдено созвездий: " + constellationsCount.ToString());

                for (int i = 0; i < constellationsList.Length; i++)
                {
                    var constellations = await Universe.GetConstellation(constellationsList[i], "ru");
                    listBox1.Items.Add(constellationsList[i].ToString() + " " 
                        + constellations.name + " [Регион:" + constellations.region_id + "] --- Ok");

                    listBox1.Items.Add("");
                    progressBar2.PerformStep(); //вызываем этот метод для увеличения шкалы progressBar
                }
                listBox1.Items.Add("Созвездия загружены" + constellationsCount.ToString());
            }
            else
            {
                listBox1.Items.Add("Нет созвездий");
            }

        }
        private async void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Начинаем загрузку Солнечных систем");
            var systemsList = await Universe.GetUniverse("systems");


            if (systemsList != null)
            {
                var systemsCount = systemsList.Length;
                progressBar3.Minimum = 0; // по умолчанию
                progressBar3.Maximum = systemsCount; //по умолчанию
                progressBar3.Step = 1; //по умолчанию
                listBox1.Items.Add("Найдено солнечных систем: " + systemsCount.ToString());

                for (int i = 0; i < systemsList.Length; i++)
                {
                    var systems = await Universe.GetSystem(systemsList[i], "ru");
                    listBox1.Items.Add(systemsList[i].ToString() + " "
                        + systems.name + " [Созвездие:" + systems.constellation_id + "] --- Ok");

                    listBox1.Items.Add("");
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