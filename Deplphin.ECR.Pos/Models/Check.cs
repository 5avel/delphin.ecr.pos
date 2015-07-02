using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deplphin.ECR.Pos.MVVMLib;
using System.Collections.ObjectModel;

namespace Deplphin.ECR.Pos.Models
{
    public class Check : ObservableObject
    {
        private static Check сheck;

        public static Check GetInstance()
        {
            // для исключения возможности создания двух объектов 
            // при многопоточном приложении
            if (сheck == null)
            {
                lock (typeof(Check))
                {
                    if (сheck == null)
                        сheck = new Check();
                }
            }
            return сheck;
        }

        private Check()
        {

        }


        private ObservableCollection<CheckItem> checkItems = new ObservableCollection<CheckItem>();
        public ObservableCollection<CheckItem> CheckItems
        {
            get { return checkItems; }
            set
            {
                checkItems = value;
              //  UpdateSceckSum();
                OnPropertyChanged(() => CheckItems);
            }
        }


        private double checkSum;
        public double CheckSum // Сумма чека
        {
            get { return checkSum; } set {checkSum = value; OnPropertyChanged(() => CheckSum); }
        }

        private double checkSumWithoutDisc;
        public double СheckSumWithoutDisc // Сумма чека без скидки
        {
            get { return checkSumWithoutDisc; }
            set { checkSumWithoutDisc = value; OnPropertyChanged(() => СheckSumWithoutDisc); }
        }

        private double paySum;
        public double PaySum // Уплочено
        {
            get { return paySum; }
            set { paySum = value; UpdateSceckSum(); OnPropertyChanged(() => PaySum); }
        }

        private double discSum;
        public double DiscSum // Сумма скидки
        {
            get { return discSum; }
            set { discSum = value; OnPropertyChanged(() => DiscSum); }
        }

        private int discType;
        /// <summary>
        /// Тип Скидки. 0 - Процентная, 1 - Сумовая.
        /// </summary>
        public int DiscType 
        {
            get { return discType; }
            set { discType = value; OnPropertyChanged(() => DiscType); }
        }

        private double discont;
        public double Discont // скидка Сумовая или %
        {
            get { return discont; }
            set { discont = value; UpdateSceckSum(); OnPropertyChanged(() => Discont); }
        }

        private double costSum;
        public double CostSum  // Здача
        {
            get { return costSum; }
            set { costSum = value; OnPropertyChanged(() => CostSum); }
        }

        private CheckItem selectedCheckItem;

        public CheckItem SelectedCheckItem
        {
            get { return selectedCheckItem; }
            set { selectedCheckItem = value; OnPropertyChanged(() => SelectedCheckItem); }
        }



        public void AddItem(Good good)
        {


            foreach (CheckItem c in CheckItems)
            {
                if (c.Code == good.Code)
                {
                    c.Count++;
                    return;
                }
            }

            CheckItems.Add(new CheckItem
            {
                Code = good.Code,
                Count = 1,
                GoodsGroupeId = good.GoodsGroupeId,
                Name = good.Name,
                Price = good.Price
            });

            UpdateSceckSum();
        }
       

        public void UpdateSceckSum()
        {
            double sum = 0;
            foreach (CheckItem c in checkItems)
            {
                sum += c.Sum;
            }
         
            СheckSumWithoutDisc = sum; // Сумма чека без скидки

            if(DiscType == 0) // Процентная скидка
            {
                DiscSum = (sum / 100) * Discont; // Сумма скидки 
                CheckSum = sum - DiscSum; // Сумма чека с учетом процентной скидки
            }
            else if(DiscType == 1)
            {
                DiscSum = Discont; // Сумма скидки 
                CheckSum = sum - DiscSum; // Сумма чека с учетом процентной скидки
            }

            CostSum = CheckSum - PaySum; // Здача

        }

        public void Clear()
        {
            CheckItems.Clear();
            CheckSum = 0;
        }
    }
}
