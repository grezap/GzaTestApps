namespace StarRocksClient.Models
{
    public class Product
    {
        public int p_key { get; set; }
        public string p_name { get; set; }
        public string p_brand { get; set; }
        public string p_type { get; set; }
        public int p_size { get; set; }
        public string p_container { get; set; }
        public decimal p_retailprice { get; set; }
        public string p_comment { get; set; }
    }
}
