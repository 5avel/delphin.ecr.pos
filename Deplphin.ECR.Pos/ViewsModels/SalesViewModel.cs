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
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;


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
        private CheckItem selectedCheckItem;

        public CheckItem SelectedCheckItem
        {
            get { return selectedCheckItem; }
            set 
            { 
                selectedCheckItem = value;
                UpdateCheckSum();
                RaisePropertyChanged(() => SelectedCheckItem);
            }
        }

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
                UpdateCheckSum();
                RaisePropertyChanged(() => PayhSum);
                RaisePropertyChanging(() => PayhSum);
            }
        }

        private double costSum;

        public double CostSum
        {
            get { return costSum; }
            set 
            {
                costSum = value;
                RaisePropertyChanged(() => CostSum);
            }
        }

        private double discPrc;

        public double DiscPrc
        {
            get { return discPrc; }
            set 
            { 
                discPrc = value;
                UpdateCheckSum();
                RaisePropertyChanged(() => DiscPrc);
            }
        }


        private ObservableCollection<string> saleType;

        private int saleTypeIndex;
        public int SaleTypeIndex
        {
            get { return saleTypeIndex; }
            set
            {
                saleTypeIndex = value;
                RaisePropertyChanged(() => SaleTypeIndex);
            }
        }

        public SalesViewModel() // КОНСТРУКТОР
        {
            cm = ConfigurationManager.GetInstance();
            GoodsGroupeCollection = cm.GoodsGroupeCollection;
            if (cm.SelectedGroup != null) SelectedGroup = cm.SelectedGroup;
            if (cm.Check != null) Check = cm.Check;

            SaleTypeIndex = 0;


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
                check = value;
                cm.Check = value;
                UpdateCheckSum();
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
            if (discPrc != 0)
            {
                ChechSum = sum;
                ChechSum -= DiscPrc;
            }
            else
            {
                ChechSum = sum;
            }

            CostSum = ChechSum - PayhSum;
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


            UpdateCheckSum();
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
                        KKM.Sale(Check, PayhSum, SaleTypeIndex, DiscPrc);
                        Check.Clear();
                        PayhSum = 0;
                        DiscPrc = 0;
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



        private ICommand delCheckSelectrdItemCommand;
        public ICommand DelCheckSelectrdItemCommand
        {
            get
            {
                return delCheckSelectrdItemCommand ?? (delCheckSelectrdItemCommand = new RelayCommand(() =>
                {
                    Check.Remove(SelectedCheckItem);
                }));
            }
        }


        private ICommand clearCheckCommand;
        public ICommand ClearCheckCommand
        {
            get
            {
                return clearCheckCommand ?? (clearCheckCommand = new RelayCommand(() =>
                {
                    Check.Clear();
                }));
            }
        }

        private ICommand enterPayhSumCommand;
        public ICommand EnterPayhSumCommand
        {
            get
            {
                return enterPayhSumCommand ?? (enterPayhSumCommand = new RelayCommand(() =>
                {
                    UpdateCheckSum();
                }));
            }
        }



    }

}
