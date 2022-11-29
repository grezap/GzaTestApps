using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarRocksClient.Models
{
    public class Customer
    {
        public int c_key { get; set; }
        public string c_name { get; set; }
        public string c_address { get; set; }
        public int MyProperty { get; set; }
    }
}
