namespace StarRocksClient.Models
{
    public class Customer
    {
        public int c_key { get; set; }
        public string c_name { get; set; }
        public string c_address { get; set; }
        public string c_nationality { get; set; }
        public string c_phone { get; set; }
        public decimal c_activitybalance { get; set; }
        public string  c_segment { get; set; }
        public string c_comment { get; set; }
    }
}
