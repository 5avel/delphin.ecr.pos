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
    class InOutWindowViewModel : ViewModelBase
    {
        public InOutWindowViewModel()
        {
            
        }

        private double inOutSum;

        public double InOutSum
        {
            get { return inOutSum; }
            set 
            {
                inOutSum = value;
                RaisePropertyChanged(() => InOutSum);
            }
        }


        private ICommand inCommand;
        public ICommand InCommand
        {
            get
            {
                return inCommand ?? (inCommand = new RelayCommand(() =>
                {
                    KKM.PrintInOut(InOutSum, true);
                    InOutSum = 0;
                }));
            }
        }


        private ICommand outCommand;
        public ICommand OutCommand
        {
            get
            {
                return outCommand ?? (outCommand = new RelayCommand(() =>
                {
                    KKM.PrintInOut(InOutSum, false);
                    InOutSum = 0;
                }));
            }
        }

    }
}
