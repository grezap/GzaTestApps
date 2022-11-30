namespace StarRocksClient.Models
{
    public class LineItem
    {
        public int l_key { get; set; }
        public int l_orderkey { get; set; }
        public int l_linenumber { get; set; }
        public int l_prodkey { get; set; }
        public int l_suppkey { get; set; }
        public decimal l_quantity { get; set; }
        public decimal l_extendedprice { get; set; }
        public decimal l_discount { get; set; }
        public decimal l_tax { get; set; }
        public bool l_returnflag { get; set; }
        public bool l_returnstatus { get; set; }
        public DateTime l_commitdate { get; set; }
        public DateTime l_receiptdate { get; set; }
        public string l_shipinstruct { get; set; }
        public string l_shipmode { get; set; }
        public string l_comment { get; set; }
    }
}
