using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deplphin.ECR.Pos.DAL;

namespace Deplphin.ECR.Pos.Models
{
    public class CheckItem : ObservableObject
    {

        private string name;
        private int code;
        private double price;
        private int goodsGroupeId;
        private double count;
        private double sum;

        public double Sum
        {
            get { return sum; }
            set 
            { 
                sum = value;
                RaisePropertyChanged(() => Sum);
            }
        }

        public double Count
        {
            get { return count; }
            set 
            { 
                count = value;
                Sum = Count * Price;
                RaisePropertyChanged(() => Count);
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
                RaisePropertyChanged(() => Price);
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
