using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class MainViewModel : ViewModelBase
    {
        private string frameSource;

        public string FrameSource
        {
            get { return frameSource; }
            set 
            {
                frameSource = value;
                RaisePropertyChanged(() => FrameSource);
            }
        }




        private ICommand _windowsSales;
        public ICommand WindowsSales
        {
            get
            {
                return _windowsSales ?? (_windowsSales = new RelayCommand(() =>
                    {
                        FrameSource = "SalesView.xaml";
                    }));
            }
        }

        private ICommand _windowsGoods;
        public ICommand WindowsGoods
        {
            get
            {
                return _windowsGoods ?? (_windowsGoods = new RelayCommand(() =>
                {
                    FrameSource = "GoodsView.xaml";
                }));
            }
        }
    }
}
