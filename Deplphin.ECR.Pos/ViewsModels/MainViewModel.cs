using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Deplphin.ECR.Pos.Models;
using Deplphin.ECR.Pos.DAL;

namespace Deplphin.ECR.Pos.ViewsModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            FrameSource = "SalesView.xaml"; // вид при старте
        }
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

        private ICommand printZReportCommand;
        public ICommand PrintZReportCommand
        {
            get
            {
                return printZReportCommand ?? (printZReportCommand = new RelayCommand(() =>
                {
                    KKM.PrintZReport();
                }));
            }
        }

        private ICommand printXReportCommand;
        public ICommand PrintXReportCommand
        {
            get
            {
                return printXReportCommand ?? (printXReportCommand = new RelayCommand(() =>
                {
                    KKM.PrintXReport();
                }));
            }
        }

        private ICommand emptyReceiptCommand;
        public ICommand EmptyReceiptCommand
        {
            get
            {
                return emptyReceiptCommand ?? (emptyReceiptCommand = new RelayCommand(() =>
                {
                    KKM.PrintEmptyReceipt();
                }));
            }
        }

        private ICommand copyLastReceiptCommand;
        public ICommand CopyLastReceiptCommand
        {
            get
            {
                return copyLastReceiptCommand ?? (copyLastReceiptCommand = new RelayCommand(() =>
                {
                    KKM.PrintCopyLastReceipt();
                }));
            }
        }

        private ICommand inOutCommand;
        public ICommand InOutCommand
        {
            get
            {
                return inOutCommand ?? (inOutCommand = new RelayCommand(() =>
                {
                    Views.InOutWindowView inOutView = new Views.InOutWindowView();
                    inOutView.ShowInTaskbar = false;
                    inOutView.Show();
                }));
            }
        }
    }
}
