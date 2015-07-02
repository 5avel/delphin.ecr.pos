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
using System.Diagnostics;
using System.Globalization;


namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {
        private Check check = Check.GetInstance();
        public Check Check
        {
            get { return check; }
            set { check = value; RaisePropertyChanged(() => Check); }
        }

      
        private ConfigurationManager cm = ConfigurationManager.GetInstance(); // Одиночька Конфигурационный менеджер

        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set { goodsCollection = value; RaisePropertyChanged(() => GoodsCollection); }
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
                RaisePropertyChanged(() => SelectedGroup);
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
