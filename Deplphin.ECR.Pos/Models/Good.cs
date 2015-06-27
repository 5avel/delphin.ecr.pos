﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deplphin.ECR.Pos.Models
{
    class Good
    {
        private string name;
        private int code;
        private double price;
        private int goodsGroupeId;

        public int GoodsGroupeId
        {
            get { return goodsGroupeId; }
            set { goodsGroupeId = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
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