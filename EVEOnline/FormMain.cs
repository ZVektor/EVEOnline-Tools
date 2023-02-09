using EVEOnline.Logic.ServicesAPI.Interfaces;
using EVEOnline.Logic.ServicesAPI;
using EVEOnline.Data.Models;
using EVEOnline.Logic.ServicesDB;
using EVEOnline.Logic.ServicesDB.Interfaces;

namespace EVEOnline
{
    public partial class FormMain : Form
    {
        //private readonly IUniverse _universe;
        //private readonly IRegionService _regionService;

        MyEveonlineDbContext DbContext_dbContext = new MyEveonlineDbContext();
        HttpClient httpClient = new HttpClient();


        public FormMain()
        {
            InitializeComponent();
        }

        private async void btnAddRegion_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("�������� �������� ��������");
            IUniverse universe = new Universe(httpClient);//���� ��� �� ���������� ���� �� �������� ������ ���!
            int[] regionList = await universe.GetUniverseAsync("regions");

            if (regionList != null)
            {
                var regionCount = regionList.Length;
                progressBar1.Maximum = regionCount; //������������ ��������
                listBox1.Items.Add("������� ��������: " + regionCount.ToString());

                for (int i = 0; i < regionCount; i++)
                {
                    IRegionService regionService = new RegionService(DbContext_dbContext); //���� ��� �� ���������� ���� �� �������� ������ ���!
                    Uregion data = await regionService.GetRegion(regionList[i]);

                    if (data != null)
                    {
                        listBox1.Items.Add("������: " + regionList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- ���� � ����");
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
                progressBar2.Maximum = constellationsCount; //�� ���������
                listBox1.Items.Add("������� ���������: " + constellationsCount.ToString());

                for (int i = 0; i < constellationsCount; i++)
                {
                    IConstellationService constellationService = new �onstellationService(DbContext_dbContext); //���� ��� �� ���������� ���� �� �������� ������ ���!
                    Uconstellation data = await constellationService.GetConstellation(constellationsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("���������: " + constellationsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- ���� � ����");
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
                            listBox1.Items.Add("���������: [" + constellationsList[i] + ":" + constellation.name + "] --- �������� � ����!");
                        else
                            listBox1.Items.Add("���������: [" + constellationsList[i] + ":" + constellation.name + "] --- �� ���� �������� � ���� !!!");
                    }




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
                progressBar3.Maximum = systemsCount; //�� ���������
                listBox1.Items.Add("������� ��������� ������: " + systemsCount.ToString());

                for (int i = 0; i < systemsCount; i++)
                {
                    ISystemService systemService = new SystemService(DbContext_dbContext); //���� ��� �� ���������� ���� �� �������� ������ ���!
                    Usystem data = await systemService.GetSystem(systemsList[i]);


                    if (data != null)
                    {
                        listBox1.Items.Add("��������� �������: " + systemsList[i].ToString() + " [" + data.Id + ":" + data.Name + "] --- ���� � ����");
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
                            listBox1.Items.Add("���������: [" + systemsList[i] + ":" + system.name + "] --- �������� � ����!");
                        else
                            listBox1.Items.Add("���������: [" + systemsList[i] + ":" + system.name + "] --- �� ���� �������� � ���� !!!");
                    }
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