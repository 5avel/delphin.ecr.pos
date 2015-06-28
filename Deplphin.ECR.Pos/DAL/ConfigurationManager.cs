using Deplphin.ECR.Pos.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

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
                mySet.Close();
            }
            else
            {
                MessageBox.Show("Файл: " + AppDomain.CurrentDomain.BaseDirectory + "Groups.xml" + " Необнаружен!");
            }
        }
    }
}
