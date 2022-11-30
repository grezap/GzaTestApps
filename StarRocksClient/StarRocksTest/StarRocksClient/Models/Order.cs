namespace StarRocksClient.Models
{
    public class Order
    {
        public int o_key { get; set; }
        public DateTime o_date { get; set; }
        public int o_custkey { get; set; }
        public bool o_orderstatus { get; set; }
        public decimal o_totalprice { get; set; }
        public string o_orderpriority { get; set; }
        public string o_clerk { get; set; }
        public int o_shippriority { get; set; }
        public string o_comment { get; set; }
    }
}
