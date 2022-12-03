using StarRocksClient.Db.Attributes;
using System.Globalization;

namespace StarRocksClient.Models
{
    [TableName("customer")]
    public class Customer
    {
        #region Fields
        private string _name = "Customer_";
        private string _address = "Address -, TK. 1025- Athens Greece";
        private string _nationality = "Greek";
        #endregion

        #region Constructor
        public Customer(int pkey)
        {
            SetProperties(pkey);
        }
        #endregion

        #region Properties
        [PrimaryKey("c_key")]
        [Selectable("c_key")]
        [Insertable("c_key")]
        public int c_key { get; set; }

        [Selectable("c_name")]
        [Insertable("c_name")]
        public string c_name { get; set; }

        [Selectable("c_address")]
        [Insertable("c_address")]
        public string c_address { get; set; }

        [Selectable("c_nationality")]
        [Insertable("c_nationality")]
        public string c_nationality { get; set; }

        [Selectable("c_phone")]
        [Insertable("c_phone")]
        public string c_phone { get; set; }

        [Selectable("c_activitybalance")]
        [Insertable("c_activitybalance")]
        public decimal c_activitybalance { get; set; }

        [Selectable("c_segment")]
        [Insertable("c_segment")]
        public string c_segment { get; set; }

        [Selectable("c_comment")]
        [Insertable("c_comment")]
        public string c_comment { get; set; }
        #endregion

        #region Methods
        private void SetProperties(int pkey) 
        {

            var r = new Random();
            var phonenumbers = Enumerable.Range(0, 9).ToList();

            var numbers = Enumerable.Range(0, 100).ToList();

            
            c_key = pkey;
            c_name = $"{_name}{pkey}";
            c_address = _address.Replace("-", pkey.ToString());
            c_nationality = _nationality;
            c_phone = $"+30{(String.Join("",Enumerable.Range(0,9).Select(e => phonenumbers[r.Next(phonenumbers.Count)])))}";
            c_activitybalance = Convert.ToDecimal($"{numbers.OrderBy(n => r.Next()).Take(1).First()}{numbers.OrderBy(n => r.Next()).Take(1).First()}.{numbers.OrderBy(n => r.Next()).Take(1).First()}",CultureInfo.InvariantCulture);
            c_segment = $"Segment{pkey}";
            c_comment = $"Comment For {_name}: Call On {c_phone} on inactive hours.";
        }
        #endregion
    }
}
