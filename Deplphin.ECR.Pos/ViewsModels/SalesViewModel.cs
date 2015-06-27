using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

using Deplphin.ECR.Pos.Models;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {

        public SalesViewModel()
        {
            GetGoodsFromFile(); // Загружаем товары из xml файла
            GetGroupsFromFile(); // Загружаем Группы товаров из xml файла


            //SelectedGroup = GoodsGroupeCollection[0];


            //XmlSerializer myXmlSer = new XmlSerializer(GoodsCollection.GetType());
            //StreamWriter myWriter = new StreamWriter(Environment.CurrentDirectory + @"\Goods.xml");
            //myXmlSer.Serialize(myWriter, GoodsCollection);
            //myWriter.Close();



            //XmlSerializer myXmlSer = new XmlSerializer(GoodsGroupeCollection.GetType());
            //StreamWriter myWriter = new StreamWriter(Environment.CurrentDirectory + @"\Groups.xml");
            //myXmlSer.Serialize(myWriter, GoodsGroupeCollection);
            //myWriter.Close();

        }
        // Все товары
        public ObservableCollection<Good> AllGoodsCollection { get; private set; }

        // ТОвары выбранной группы
        private ObservableCollection<Good> goodsCollection;

        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set
            { 
                goodsCollection = value;
                RaisePropertyChanged(() => GoodsCollection);
            }
        }



        // Группы товаров
        public ObservableCollection<GoodsGroup> GoodsGroupeCollection { get; private set; }

        private GoodsGroup selectedGroup;

        public GoodsGroup SelectedGroup
        {
            get { return selectedGroup; }
            set 
            {
                selectedGroup = value;
                SetGoodsSelectedGrop(); // Выводит товары выбранной группы
                RaisePropertyChanged(() => SelectedGroup);
                
            }
        }

        private Good selectedGood;

        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;
                AddGoodToCheck(SelectedGood); // Добавляем товар в чек
                selectedGood = null;
                //RaisePropertyChanged(() => SelectedGood);

            }
        }

        // Чек
        private ObservableCollection<CheckItem> check;

        public ObservableCollection<CheckItem> Check
        {
            get { return check; }
            set
            {
                check = value;
                RaisePropertyChanged(() => Check);
            }
        } 



        void GetGoodsFromFile()
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

        void GetGroupsFromFile()
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

        void SetGoodsSelectedGrop()
        {
            int count = AllGoodsCollection.Count;
            GoodsCollection = new ObservableCollection<Good>();
            for (int i = 0; i <= count; i++)
            {
                if (AllGoodsCollection[i].GoodsGroupeId == SelectedGroup.Code)
                {
                    GoodsCollection.Add(AllGoodsCollection[i]);
                }
            }
        }

        void AddGoodToCheck(Good good)
        {
            if (Check == null) Check = new ObservableCollection<CheckItem>();

            foreach (CheckItem c in Check)
            {
                if (c.Code == good.Code)
                {
                    c.Count++;
                    return;
                }
            }


            Check.Add(new CheckItem
            {
                Code = good.Code,
                Count = 1,
                GoodsGroupeId = good.GoodsGroupeId,
                Name = good.Name,
                Price = good.Price
            });

            
        }


        private ICommand goodDoublClick;
        public ICommand GoodDoublClick
        {
            get
            {
                return goodDoublClick ?? (goodDoublClick = new RelayCommand(() =>
                {
                   
                }));
            }
        }

    }

}
