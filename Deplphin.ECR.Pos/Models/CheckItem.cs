﻿
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Deplphin.ECR.Pos.MVVMLib;

namespace Deplphin.ECR.Pos.Models
{
    public class CheckItem : ObservableObject
    {
        Check ch;

        private string name;
        private int code;
        private double price;
        private int goodsGroupeId;
        private double count;
        private double sum;

        public CheckItem()
        {
            ch = Check.GetInstance();
        }

        public double Sum
        {
            get { return sum; }
            set 
            {  
                sum = value;
                ch.UpdateSceckSum();
                OnPropertyChanged(() => Sum);
            }
        }

        public double Count
        {
            get { return count; }
            set 
            {
                count = value;
                Sum = Count * Price;
                OnPropertyChanged(() => Count);
            }
        }

        public int GoodsGroupeId
        {
            get { return goodsGroupeId; }
            set { goodsGroupeId = value; }
        }

        public double Price
        {
            get { return price; }
            set 
            {
                price = value;
                Sum = Count * Price;
                OnPropertyChanged(() => Price);
            }
        }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
