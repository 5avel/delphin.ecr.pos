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

using System.Reflection;
using ecrmini;


namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {
        private ConfigurationManager cm;
        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        public ObservableCollection<GoodsGroup> GoodsGroupeCollection { get; private set; } // Группы товаров
        private GoodsGroup selectedGroup; // Выбранная группа
        private Good selectedGood; // Выбранный товар
        private ObservableCollection<CheckItem> check; // Чек

        private double chechSum;

        public double ChechSum
        {
            get { return chechSum; }
            set 
            { 
                chechSum = value;
                RaisePropertyChanged(() => ChechSum);
            }
        }

        private double paySum;

        public double PayhSum
        {
            get { return paySum; }
            set
            {
                paySum = value;
                RaisePropertyChanged(() => PayhSum);
            }
        }

        public SalesViewModel() // КОНСТРУКТОР
        {
            cm = ConfigurationManager.GetInstance();
            GoodsGroupeCollection = cm.GoodsGroupeCollection;
            if(cm.SelectedGroup != null) SelectedGroup = cm.SelectedGroup;
            if (cm.Check != null) Check = cm.Check;
        }



        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set
            { 
                goodsCollection = value;
                RaisePropertyChanged(() => GoodsCollection);
            }
        }

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

        public ObservableCollection<CheckItem> Check
        {
            get { return check; }
            set
            {
                MessageBox.Show("sds");
                check = value;
                cm.Check = value;
                
                RaisePropertyChanged(() => Check);

            }
        } 

        public void UpdateCheckSum()
        {
            double sum = 0;
            foreach(CheckItem c in Check)
            {
                sum += c.Sum;
            }
            ChechSum = sum;
        }


        private void SetGoodsSelectedGrop()
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

        private void AddGoodToCheck(Good good)
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


        private ICommand checkoutCommand;
        public ICommand CheckoutCommand
        {
            get
            {
                return checkoutCommand ?? (checkoutCommand = new RelayCommand(() =>
                {
                    if (Check != null && Check.Count > 0)
                    {
                        KKM.Sale(Check, PayhSum);
                        Check.Clear();
                        
                    }

                }));
            }
        }

        private ICommand updateCheckSumCommand;
        public ICommand UpdateCheckSumCommand
        {
            get
            {
                return updateCheckSumCommand ?? (updateCheckSumCommand = new RelayCommand(() =>
                {
                        UpdateCheckSum();
                }));
            }
        }

    }

}
