using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class SalesViewModel : ViewModelBase
    {
        public SalesViewModel()
        {

            Root = new GroupItem() { Title = "Menu" };
            GroupItem childItem1 = new GroupItem() { Title = "Child item #1" };

            childItem1.Items.Add(new GroupItem() { Title = "Child item #1.1" });
            childItem1.Items.Add(new GroupItem() { Title = "Child item #1.2" });
            childItem1.Goods.Add(new Good() { Name = "test", Price = 6.95 });
            GroupItem childItem2 = new GroupItem() { Title = "Child item #1" };
            childItem2.Items.Add(new GroupItem() { Title = "Child item #1.1" });
            childItem2.Items.Add(new GroupItem() { Title = "Child item #1.2" });
            childItem2.Goods.Add(new Good() { Name = "test", Price = 6.95 });
            childItem2.Goods.Add(new Good() { Name = "test", Price = 6.95 });

            childItem1.Items.Add(childItem2);
            Root.Items.Add(childItem1);
            Root.Items.Add(new GroupItem() { Title = "Child item #2" });
            Root.Goods.Add(new Good() { Name = "test", Price = 6.95 });

        }

        private GroupItem root;

        public GroupItem Root
        {
            get { return root; }
            set
            {
                root = value;
                RaisePropertyChanged(() => Root);
            }
        }
    }

    public class GroupItem
    {
        public GroupItem()
        {
            this.Items = new ObservableCollection<GroupItem>();

            this.Goods = new ObservableCollection<Good>();
        }

        public string Title { get; set; }

        public ObservableCollection<GroupItem> Items { get; set; }
        public ObservableCollection<Good> Goods { get; set; }

    }

    public class Good
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
