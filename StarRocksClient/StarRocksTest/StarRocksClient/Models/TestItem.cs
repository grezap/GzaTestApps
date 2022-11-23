namespace StarRocksClient.Models
{
    public class TestItem
    {
        #region Constructor
        public TestItem(){}
        public TestItem(int i)
        {
            Id = i++;
            Name = $"ProductName{i}";
            OrderDate = DateTime.Today.AddDays(i - (i * 3)).ToString("yyyy-MM-dd HH:mm:ss");
            ItemPrice = (30 + i)/2;
            ItemInStock = i % 2 == 0 ? true : false ;
            ItemWarehouse = $"Warehouse - {i}";
            ItemRevenue = ItemPrice * (10 / 100);
            ItemQuantity = 2 + (i % 3);
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderDate { get; set; }

        public double ItemPrice { get; set; }
        public bool ItemInStock { get; set; }
        public string ItemWarehouse { get; set; }
        public double ItemRevenue { get; set; }
        public int ItemQuantity { get; set; } 
        #endregion
    }
}
