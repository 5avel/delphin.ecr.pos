
using Deplphin.ECR.Pos.Models;
using ecrmini;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Deplphin.ECR.Pos.DAL
{
    public class KKM
    {
        private static t400 t;

        static KKM()
        {
            t = new t400();
        }

        public static bool PrintZReport()
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            t.t400me("execute_report;z1;12321;");
            t.t400me("close_port;");
            return true; 
        }

        public static bool PrintXReport()
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            t.t400me("execute_report;x1;12321;");
            t.t400me("close_port;");
            return true;
        }

        public static bool CancelReceipt()
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            t.t400me("cancel_receipt;");
            t.t400me("close_port;");
            return true;
        }

        public static bool PrintEmptyReceipt()
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            t.t400me("print_empty_receipt;");
            t.t400me("close_port;");
            return true;
        }

        public static bool PrintCopyLastReceipt()
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            t.t400me("print_receipt_copy;");
            t.t400me("close_port;");
            return true;
        }

        public static bool PrintInOut(double sum, bool isIn_notOut = true)
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");
            if (isIn_notOut) { t.t400me("in_out;0;0;0;0;" + sum.ToString("N2") + ""); }
            else {             t.t400me("in_out;0;0;0;1;" + sum.ToString("N2") + ""); }
            t.t400me("close_port;");
            return true;
        }

        
        

        public static bool Test()
        {
            //Console.WriteLine(t.t400me("open_port;4;115200;"));
            //t.t400me("cashier_registration;1;0;");
            //t.t400me("add_plu;4;0;1;0;0;0;1;100.00;111111;ТОВАР6789012345678901234567890123456789012345678;100000;");

            //t.t400me("open_receipt;0");
            //t.t400me("sale_plu;0;0;0;2.000;4");
            //t.t400me("pay;0;0");

            //t.t400me("close_port;");

            
            return true;
        }

        public static bool Sale(ObservableCollection<CheckItem> Check, double paySum, int payType, double discontPrc)
        {
            t.t400me("open_port;4;115200;");
            t.t400me("cashier_registration;1;0;");

            t.t400me("open_receipt;0");
            foreach(CheckItem c in Check)
            {
                t.t400me("add_plu;"+c.Code.ToString()+";0;1;0;0;0;1;"+c.Price.ToString()+";111111;"+c.Name+";100000;");
                t.t400me("sale_plu;0;0;0;"+c.Count.ToString()+";"+c.Code.ToString()+"");
            }


            t.t400me("discount_surcharge;0;1;1;" + discontPrc.ToString("N2") + ";");
            
            t.t400me("pay;"+payType.ToString()+";"+paySum.ToString("N2")+"");

            t.t400me("close_port;");
            return true;
        }


    }
}
