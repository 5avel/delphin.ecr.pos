using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

using Deplphin.ECR.Pos.Models;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Deplphin.ECR.Pos.DAL;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {
        public ConfigurationManager cm;
        public SalesViewModel()
        {
            cm = ConfigurationManager.GetInstance();
            GoodsGroupeCollection = cm.GoodsGroupeCollection;
            if(cm.SelectedGroup != null) SelectedGroup = cm.SelectedGroup;
            if (cm.Check != null) Check = cm.Check;

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

        }
        // Все товары
        //public ObservableCollection<Good> AllGoodsCollection { get; private set; }

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
                cm.SelectedGroup = value;
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
                cm.Check = value;
                RaisePropertyChanged(() => Check);
            }
        } 





        void SetGoodsSelectedGrop()
        {
            int count = cm.AllGoodsCollection.Count;
            GoodsCollection = new ObservableCollection<Good>();
            for (int i = 0; i < count; i++)
            {
                if (cm.AllGoodsCollection[i].GoodsGroupeId == SelectedGroup.Code)
                {
                    GoodsCollection.Add(cm.AllGoodsCollection[i]);
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
