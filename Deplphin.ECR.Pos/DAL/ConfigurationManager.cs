using Deplphin.ECR.Pos.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using System.Linq;

namespace Deplphin.ECR.Pos.DAL
{
    public class ConfigurationManager
    {
        private static ConfigurationManager configManager;

        private ConfigurationManager()
        {
            GetGoodsFromFile(); // Загружаем товары из xml файла
            GetGroupsFromFile(); // Загружаем Группы товаров из xml файла
        }

        public static ConfigurationManager GetInstance()
        {
            // для исключения возможности создания двух объектов 
            // при многопоточном приложении
            if (configManager == null)
            {
                lock (typeof(ConfigurationManager))
                {
                    if (configManager == null)
                        configManager = new ConfigurationManager();
                }
            }

            return configManager;
        }


        // Все товары
        public ObservableCollection<Good> AllGoodsCollection { get; set; }

        // Группы товаров
        public ObservableCollection<GoodsGroup> GoodsGroupeCollection { get; set; }



        public GoodsGroup SelectedGroup { get; set;}
        public ObservableCollection<CheckItem> Check { get; set; }

        private void GetGoodsFromFile()
        {
            AllGoodsCollection = new ObservableCollection<Good>();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Goods.xml"))
            {
                XmlSerializer myXmlSer = new XmlSerializer(typeof(ObservableCollection<Good>));
                FileStream mySet = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Goods.xml", FileMode.Open);
                AllGoodsCollection = (ObservableCollection<Good>)myXmlSer.Deserialize(mySet);

                // Sorting
                var ggc = new ObservableCollection<Good>(from i in AllGoodsCollection orderby i.Code select i);
                AllGoodsCollection = ggc;

                mySet.Close();

            }
            else
            {
                MessageBox.Show("Файл: " + AppDomain.CurrentDomain.BaseDirectory + "Goods.xml" + " Необнаружен!");
            }
        }

        private void GetGroupsFromFile()
        {
            GoodsGroupeCollection = new ObservableCollection<GoodsGroup>();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Groups.xml"))
            {
                XmlSerializer myXmlSer = new XmlSerializer(typeof(ObservableCollection<GoodsGroup>));
                FileStream mySet = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Groups.xml", FileMode.Open);
                GoodsGroupeCollection = (ObservableCollection<GoodsGroup>)myXmlSer.Deserialize(mySet);
                // Sorting
                var ggc = new ObservableCollection<GoodsGroup>(from i in GoodsGroupeCollection orderby i.Code select i);
                GoodsGroupeCollection = ggc;

                mySet.Close();
            }
            else
            {
                MessageBox.Show("Файл: " + AppDomain.CurrentDomain.BaseDirectory + "Groups.xml" + " Необнаружен!");
            }
        }

        public bool SaveDataToXml()
        {
            try
            {
                XmlSerializer myXmlSerGroupe = new XmlSerializer(GoodsGroupeCollection.GetType());
                StreamWriter myWriterGroupe = new StreamWriter(Environment.CurrentDirectory + @"\Groups.xml");
                myXmlSerGroupe.Serialize(myWriterGroupe, GoodsGroupeCollection);
                myWriterGroupe.Close();



                XmlSerializer myXmlSerGoods = new XmlSerializer(AllGoodsCollection.GetType());
                StreamWriter myWriterGoods = new StreamWriter(Environment.CurrentDirectory + @"\Goods.xml");
                myXmlSerGoods.Serialize(myWriterGoods, AllGoodsCollection);
                myWriterGoods.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
    }
}

//SelectedGroup = GoodsGroupeCollection[0];
//goodsCollection = new ObservableCollection<Good>();
//goodsCollection.Add(new Good {Code=1,GoodsGroupeId=1,Name="Test1", Price=8.8 });
//goodsCollection.Add(new Good { Code = 2, GoodsGroupeId = 1, Name = "Test2", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 3, GoodsGroupeId = 1, Name = "Test3", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 4, GoodsGroupeId = 1, Name = "Test4", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 5, GoodsGroupeId = 2, Name = "Test5", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 6, GoodsGroupeId = 2, Name = "Test6", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 7, GoodsGroupeId = 2, Name = "Test7", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 8, GoodsGroupeId = 3, Name = "Test8", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 9, GoodsGroupeId = 3, Name = "Test9", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 10, GoodsGroupeId = 3, Name = "Test10", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 11, GoodsGroupeId = 3, Name = "Test11", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 12, GoodsGroupeId = 3, Name = "Test12", Price = 8.8 });
//goodsCollection.Add(new Good { Code = 13, GoodsGroupeId = 4, Name = "Test13", Price = 8.8 });
//XmlSerializer myXmlSer = new XmlSerializer(GoodsCollection.GetType());
//StreamWriter myWriter = new StreamWriter(Environment.CurrentDirectory + @"\Goods.xml");
//myXmlSer.Serialize(myWriter, GoodsCollection);
//myWriter.Close();


//GoodsGroupeCollection = new ObservableCollection<GoodsGroup>();
//GoodsGroupeCollection.Add(new GoodsGroup { Code = 1, Name = "Товары1" });
//GoodsGroupeCollection.Add(new GoodsGroup { Code = 2, Name = "Товары1" });
//GoodsGroupeCollection.Add(new GoodsGroup { Code = 3, Name = "Товары1" });
//GoodsGroupeCollection.Add(new GoodsGroup { Code = 4, Name = "Товары1" });
//XmlSerializer myXmlSer = new XmlSerializer(GoodsGroupeCollection.GetType());
//StreamWriter myWriter = new StreamWriter(Environment.CurrentDirectory + @"\Groups.xml");
//myXmlSer.Serialize(myWriter, GoodsGroupeCollection);
//myWriter.Close();