using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deplphin.ECR.Pos.Models
{
    class GoodsGroup
    {
        private string name;
        private int code;

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
