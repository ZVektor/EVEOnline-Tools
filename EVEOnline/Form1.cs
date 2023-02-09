//using EVEOnline.Services;
using EVEOnline.Logic.ServicesAPI.Interfaces;
using EVEOnline.Logic.ServicesAPI;
using EVEOnline.Data.Models;
using System;
using Azure;
using EVEOnline.Logic.Models;
using EVEOnline.Logic.Services;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
//using EVEOnline.Services;
//using EVEOnline.Services;

namespace EVEOnline
{
    public partial class Form1 : Form
    {
        //private readonly IUniverse _universe;
        //private readonly IRegionService _regionService;

        MyEveonlineDbContext DbContext_dbContext = new MyEveonlineDbContext();
        HttpClient httpClient = new HttpClient();


        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAddRegion_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("�������� �������� ��������");
            int[] regionList = null;

            IUniverse universe = new Universe(httpClient);
            regionList = await universe.GetUniverseAsync("regions");


            if (regionList != null)
            {
                var regionCount = regionList.Length;

                progressBar1.Minimum = 0; // �� ���������
                progressBar1.Maximum = regionCount; //�� ���������
                progressBar1.Step = 1; //�� ���������

                listBox1.Items.Add("������� ��������: " + regionCount.ToString());

                for (int i = 0; i < regionList.Length; i++)
                {
                    Uregion data = new Uregion();

                    IRegionService regionService = new RegionService(DbContext_dbContext);
                    //var searchData = DbContext_dbContext.Uregions.FirstOrDefaultAsync(x => x.Id == regionList[i]);
                    //var searchData = await DbContext_dbContext.Uregions.FindAsync(1);
                    data = await regionService.GetRegion(regionList[i]);

                    if (data != null)
                    {
                        listBox1.Items.Add("������: "+ regionList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- ���� � ����");
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
                            listBox1.Items.Add("������: [" + regionList[i] + ":" + region.name + "] --- �������� � ����!");
                        else
                            listBox1.Items.Add("������: [" + regionList[i] + ":" + region.name + "] --- �� ���� �������� � ���� !!!");
                    }
                    //listBox1.Items.Add(regionList[i].ToString()+" "+region.name + ": Ok");
                    //listBox1.Items.Add(region.description);
                    //listBox1.Items.Add("");
                    progressBar1.PerformStep(); //�������� ���� ����� ��� ���������� ����� progressBar
                }
                listBox1.Items.Add("��� ������� ��������� � ����" + regionCount.ToString());
            }
            else
            {
                listBox1.Items.Add("��� ��������");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("�������� �������� ���������");
            IUniverse universe = new Universe(httpClient);

            var constellationsList = await universe.GetUniverseAsync("constellations");


            if (constellationsList != null)
            {
                var constellationsCount = constellationsList.Length;
                progressBar2.Minimum = 0; // �� ���������
                progressBar2.Maximum = constellationsCount; //�� ���������
                progressBar2.Step = 1; //�� ���������
                listBox1.Items.Add("������� ���������: " + constellationsCount.ToString());

                for (int i = 0; i < constellationsList.Length; i++)
                {
                    var constellations = await universe.GetConstellationAsync(constellationsList[i], "ru");
                    listBox1.Items.Add(constellationsList[i].ToString() + " " 
                        + constellations.name + " [������:" + constellations.region_id + "] --- Ok");

                    listBox1.Items.Add("");
                    progressBar2.PerformStep(); //�������� ���� ����� ��� ���������� ����� progressBar
                }
                listBox1.Items.Add("��������� ���������" + constellationsCount.ToString());
            }
            else
            {
                listBox1.Items.Add("��� ���������");
            }

        }
        private async void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("�������� �������� ��������� ������");
            IUniverse universe = new Universe(httpClient);
            var systemsList = await universe.GetUniverseAsync("systems");


            if (systemsList != null)
            {
                var systemsCount = systemsList.Length;
                progressBar3.Minimum = 0; // �� ���������
                progressBar3.Maximum = systemsCount; //�� ���������
                progressBar3.Step = 1; //�� ���������
                listBox1.Items.Add("������� ��������� ������: " + systemsCount.ToString());

                for (int i = 0; i < systemsList.Length; i++)
                {
                    var systems = await universe.GetSystemAsync(systemsList[i], "ru");
                    listBox1.Items.Add(systemsList[i].ToString() + " "
                        + systems.name + " [���������:" + systems.constellation_id + "] --- Ok");

                    listBox1.Items.Add("");
                    progressBar3.PerformStep();
                }
                listBox1.Items.Add("��������� ���������" + systemsCount.ToString());
            }
            else
            {
                listBox1.Items.Add("��� ���������");
            }

        }
    }
}