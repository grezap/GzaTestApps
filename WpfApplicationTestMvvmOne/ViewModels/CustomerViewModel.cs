using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplicationTestMvvmOne.Models;

namespace WpfApplicationTestMvvmOne.ViewModels
{
    public class CustomerViewModel
    {
        private Customer cust = new Customer();

        public String TxtCustomerName
        {
            get { return cust.CustomerName; }
            set { cust.CustomerName = value; }
        }

        public String TxtAmount
        {
            get { return Convert.ToString(cust.Amount); }
            set { cust.Amount = Convert.ToDouble(value); }
        }

        public String LblAmountColor
        {
            get
            {
                if (cust.Amount > 2000)
                {
                    return "Blue";
                }
                else if(cust.Amount > 1500)
                {
                    return "Red";
                }
                return "Yellow";
            }
        }

        public Boolean IsMarried
        {
            get
            {
                if (cust.IsMarried == "Married") { return true; }
                else
                {
                    return false;
                }
            }
            set {
                if (value == true) { cust.IsMarried = "Married"; }
                else { cust.IsMarried = "Not Married"; }
            }
        }

        public void CalculateTax() { cust.CalculateTax(); }
    }
}
