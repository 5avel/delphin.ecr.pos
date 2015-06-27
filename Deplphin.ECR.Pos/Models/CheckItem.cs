using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deplphin.ECR.Pos.Models
{
    public class CheckItem : ObservableObject
    {
        private string name;
        private int code;
        private double price;
        private int goodsGroupeId;
        private double count;

        public double Count
        {
            get { return count; }
            set 
            { 
                count = value;
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
