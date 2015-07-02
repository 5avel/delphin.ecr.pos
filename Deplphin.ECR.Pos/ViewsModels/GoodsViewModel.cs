using System;

using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Deplphin.ECR.Pos.Models;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Deplphin.ECR.Pos.DAL;
using Deplphin.ECR.Pos.MVVMLib;


namespace Deplphin.ECR.Pos.ViewsModels
{
    class GoodsViewModel : ViewModelBase
    {
        private ConfigurationManager cm;
        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        private ObservableCollection<GoodsGroup> goodsGroupeCollection; // Группы товаров

        public ObservableCollection<GoodsGroup> GoodsGroupeCollection
        {
            get { return goodsGroupeCollection; }
            set 
            { 
                goodsGroupeCollection = value;
            }
        }
        private GoodsGroup selectedGroup; // Выбранная группа
        private Good selectedGood; // Выбранный товар

        private bool isSelectedGroupe;

        public bool IsSelectedGroupe
        {
            get { return isSelectedGroupe; }
            set 
            { 
                isSelectedGroupe = value;
                OnPropertyChanged("IsSelectedGroupe");
            }
        }
        private bool isSelectedGood;

        public bool IsSelectedGood
        {
            get { return isSelectedGood; }
            set 
            { 
                isSelectedGood = value;
                OnPropertyChanged("IsSelectedGood");
            }
        }

        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set
            {
                goodsCollection = value;
                OnPropertyChanged("GoodsCollection");
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
                if (value == null) IsSelectedGroupe = false;
                else IsSelectedGroupe = true;
                OnPropertyChanged("SelectedGroup");
            }
        }

        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;

                if (value == null) IsSelectedGood = false;
                else IsSelectedGood = true;
                //RaisePropertyChanged(() => SelectedGood);
            }
        }

        public GoodsViewModel() // КОНСТРУКТОР
        {
            cm = ConfigurationManager.GetInstance();
            GoodsGroupeCollection = cm.GoodsGroupeCollection;
            if(cm.SelectedGroup != null) SelectedGroup = cm.SelectedGroup;
            
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

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(() =>
                {
                    if(cm.SaveDataToXml())
                    {
                        MessageBox.Show("Данные сохранены");
                    }

                }));
            }
        }


        private ICommand addGroupCommand;
        public ICommand AddGroupCommand
        {
            get
            {
                return addGroupCommand ?? (addGroupCommand = new RelayCommand(() =>
                {
                    int Code = GoodsGroupeCollection.Count + 1;
                    while(!CheckingGroupCode(Code))
                    {
                        Code++;
                    }
                    GoodsGroupeCollection.Add(new GoodsGroup
                    {
                        Code = Code,
                        Name = ""
                    });
                }));
            }
        }

        private bool CheckingGroupCode(int code)
        {
            foreach (GoodsGroup g in GoodsGroupeCollection)
            {
                if (g.Code == code) return false;
            }

            return true;
        }

        private ICommand delGroupCommand;
        public ICommand DelGroupCommand
        {
            get
            {
                return delGroupCommand ?? (delGroupCommand = new RelayCommand(() =>
                {
                    GoodsGroupeCollection.Remove(selectedGroup);

                }));
            }
        }

        private ICommand addGoodCommand;
        public ICommand AddGoodCommand
        {
            get
            {
                return addGoodCommand ?? (addGoodCommand = new RelayCommand(() =>
                {

                    int Code = cm.AllGoodsCollection.Count+1;
                    while (!CheckingGoodCode(Code))
                    {
                        Code++;
                    }
                    cm.AllGoodsCollection.Add(new Good {
                        Code = Code,
                        GoodsGroupeId = SelectedGroup.Code,
                        Name = "",
                        Price = 0
                    });
                    SetGoodsSelectedGrop();

                }));
            }
        }

        private bool CheckingGoodCode(int code)
        {
            foreach (Good g in cm.AllGoodsCollection)
            {
                if (g.Code == code) return false;
            }

            return true;
        }

        private ICommand delGoodCommand;
        public ICommand DelGoodCommand
        {
            get
            {
                return delGoodCommand ?? (delGoodCommand = new RelayCommand(() =>
                {
                    cm.AllGoodsCollection.Remove(SelectedGood);
                    SetGoodsSelectedGrop();

                }));
            }
        }

        private ICommand editGoodCommand;
        public ICommand EditGoodCommand
        {
            get
            {
                return editGoodCommand ?? (editGoodCommand = new RelayCommand(() =>
                {
                    // Редактирование товара

                }));
            }
        }
    }
}
