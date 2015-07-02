

using Deplphin.ECR.Pos.MVVMLib;
namespace Deplphin.ECR.Pos.Models
{
    public class Good : ObservableObject
    {
        private string name;
        private int code;
        private double price;
        private int goodsGroupeId;
        public static double CheckSum;

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
