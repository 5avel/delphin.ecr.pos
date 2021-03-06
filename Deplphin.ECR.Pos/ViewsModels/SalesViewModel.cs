﻿using System.Collections.ObjectModel;
using Deplphin.ECR.Pos.Models;
using System.Windows.Input;
using Deplphin.ECR.Pos.DAL;
using Deplphin.ECR.Pos.MVVMLib;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {
       // public ICommand SaveCommand { get; private set; }



        private Check check = Check.GetInstance();
        public Check Check
        {
            get { return check; }
            set { check = value; OnPropertyChanged("Check"); }
        }

      
        private ConfigurationManager cm = ConfigurationManager.GetInstance(); // Одиночька Конфигурационный менеджер

        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set { goodsCollection = value; OnPropertyChanged("GoodsCollection"); }
        }

        private Good selectedGood; // Выбранный товар
        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;
                Check.AddItem(SelectedGood); // Добавляем товар в чек
                selectedGood = null;
                
            }
        }

        public ObservableCollection<GoodsGroup> GoodsGroupeCollection { get; private set; } // Группы товаров
      
        private GoodsGroup selectedGroup; // Выбранная группа
        public GoodsGroup SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                cm.SelectedGroup = value;
                SetGoodsSelectedGrop(); // Выводит товары выбранной группы
                OnPropertyChanged("SelectedGroup");
            }
        }


        public SalesViewModel() // КОНСТРУКТОР
        {
            GoodsGroupeCollection = cm.GoodsGroupeCollection;
            if (cm.SelectedGroup != null) SelectedGroup = cm.SelectedGroup;            
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


        #region Команды SaleViewModel

        private ICommand checkoutCommand;
        public ICommand CheckoutCommand
        {
            get
            {
                return checkoutCommand ?? (checkoutCommand = new RelayCommand(() =>
                {
                    if (Check != null && Check.CheckItems.Count > 0)
                    {
                      //  KKM.Sale(Check.CheckItems, PayhSum, SaleTypeIndex, DiscPrc);
                        Check.Clear();

                    }
                    
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
                    Check.CheckItems.Remove(Check.SelectedCheckItem);
                    Check.UpdateSceckSum();
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

        #endregion Команды SaleViewModel

    }

}
