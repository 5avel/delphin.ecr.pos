

using Deplphin.ECR.Pos.MVVMLib;

namespace Deplphin.ECR.Pos.Models
{
    public class GoodsGroup : ObservableObject
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
