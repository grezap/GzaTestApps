using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationTestMvvmOne.Models
{
    public class Customer
    {

        #region Fields

        private Double _amount;
        private String _customerName;
        private String _isMarried;
        private Double _tax;
        #endregion


        public Double Amount { get { return _amount; } set { _amount = value; } }
        public String CustomerName { get { return _customerName; } set { _customerName = value; } }
        public String IsMarried { get { return _isMarried; } set { _isMarried = value; } }
        public Double Tax { get { return _tax; } set { _tax = value; } }

        #region Methods

        public void CalculateTax()
        {
            if (_amount > 2000)
            {
                _tax = 20;
            }
            else if (_amount > 1000)
            {
                _tax = 10;
            }
            else { _tax = 5; }
        }

        #endregion
    }
}
