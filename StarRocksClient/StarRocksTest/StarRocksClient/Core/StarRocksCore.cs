using Microsoft.Extensions.Configuration;
using StarRocksClient.CommandLineOptions;
using StarRocksClient.Core.Generators;

namespace StarRocksClient.Core
{
    public class StarRocksCore
    {
        #region Fields
        private static IConfiguration _config;
        private string _connStr= String.Empty;
        private int _rowNumber;
        private int _orderRows;
        private int _orderItemRows;
        #endregion

        #region Constructor
        public StarRocksCore(IConfiguration configuration, ClientOption option)
        {
            _config = configuration;
            _connStr = _config.GetConnectionString("DB");
            _rowNumber = option.RowsNumber;
            _orderRows = Convert.ToInt32(_config.GetSection("Generator").GetSection("OrderRows").Value);
            _orderItemRows = Convert.ToInt32(_config.GetSection("Generator").GetSection("OrderItemRows").Value);
        }
        #endregion

        #region Methods

        public void AllGenerator() 
        {

        }

        public void CustomerGeneration()
        {
            CustomerGenerator gen = new CustomerGenerator(_connStr, _rowNumber);
            gen.Start();
        }

        public void SupplierGeneration()
        {

        }

        public void ProductGeneration()
        {

        }

        public void OrderGeneration()
        {

        }

        public void OrderItemGeneration()
        {

        }

        //public void Start()
        //{

        //    //SimpleSelect();
        //    //SimpleInsert();
        //    int id = 5;
        //    var testitem = GetTestItemById(id);
        //    if (testitem == null)
        //    {
        //        Console.WriteLine($"No Item with id: {id} exists");
        //        testitem = new TestItem(id);
        //        var res = InsertItem(testitem);
        //        Console.WriteLine($"Inserted Item with Id {id}. Result is {res}.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Found Item with Id: {id} ");
        //    }
        //}

        //private TestItem? GetTestItemById(int id)
        //{
        //    TestItem? testItem;
        //    object parms = new { key = id };

        //    try
        //    {
        //        using (var conn = new MySqlConnection(_CONN_STR_))
        //        {
        //            testItem = conn.Query<TestItem>("select * from testitem where item_id = @key;", parms).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //        throw;
        //    }

        //    return testItem;
        //}

        //private void SimpleSelect()
        //{
        //    try
        //    {
        //        string sql = $"select item_id as Id, item_name as Name From testitem where item_id = 1";
        //        using (var conn = new MySqlConnection(_CONN_STR_))
        //        {
        //            var res = conn.Query<TestItem>(sql);
        //            if (res != null)
        //            {
        //                Console.WriteLine($"Found Item With Id: {res.First().Id}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //        throw;
        //    }
        //}

        //private int InsertItem(TestItem testItem)
        //{
        //    //(Id, Name, OrderDate, ItemPrice, ItemInStock, ItemWarehouse, ItemRevenue, ItemQuantity)
        //    string sql = "INSERT INTO testitem " +
        //                 $"values ({testItem.Id}, '{testItem.Name}', '{testItem.OrderDate}', {testItem.ItemPrice}, {(testItem.ItemInStock ? "1" : "0")}, " +
        //                 $"'{testItem.ItemWarehouse}', {testItem.ItemRevenue}, {testItem.ItemQuantity});";

        //    Console.WriteLine($"SQL Statement is: {sql}");

        //    try
        //    {
        //        using (var conn = new MySqlConnection(_CONN_STR_))
        //        {
        //            var res = conn.Execute(sql);
        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //        throw;
        //    }


        //}

        //private void SimpleInsert()
        //{
        //    string sql = "insert into testsimple (simple_key,simple_name, simple_date, simple_double,simple_tinyint,simple_nametwo,simple_doubletwo,simple_int) " +
        //        "values (3,'test2','2022-11-25 10:11:12',0.5,1,'test2',2,3)";
        //    try
        //    {
        //        using (var conn = new MySqlConnection(_CONN_STR_))
        //        {
        //            conn.Execute(sql);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //        throw;
        //    }
        //} 
        #endregion
    }
}
