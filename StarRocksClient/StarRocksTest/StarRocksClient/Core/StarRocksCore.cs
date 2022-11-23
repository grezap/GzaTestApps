using Dapper;
using MySql.Data.MySqlClient;
using StarRocksClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace StarRocksClient.Core
{
    public class StarRocksCore
    {
        #region Fields
        private readonly string _CONN_STR_ = "Server=192.168.203.8;Database=testdb;Port=9030;Uid=root;Pwd=tzimakos;";
        private static IDbConnection _conn;
        #endregion

        public StarRocksCore()
        {
            _conn = new MySqlConnection(_CONN_STR_);
        }

        public void Start() 
        {
            int id = 3;
            var testitem = GetTestItemById(id);
            if (testitem == null)
            { 
                Console.WriteLine($"No Item with id: {id} exists");
                testitem = new TestItem(id);
                var res = InsertItem(testitem);
                Console.WriteLine($"Inserted Item with Id {id}. Result is {res}.");
            }
            else
            {
                Console.WriteLine($"Found Item with Id: {id} ");
            }
        }

        private TestItem? GetTestItemById(int id) 
        {
            object parms = new { key = id };
            var testitem = _conn.QueryFirstOrDefault<TestItem>("select * from testitem where Id = @key;",parms);
            return testitem;
        }

        private int InsertItem(TestItem testItem) 
        {
            string sql = "INSERT INTO testitem (Id, Name, OrderDate, ItemPrice, ItemInStock, ItemWarehouse, ItemRevenue, ItemQuantity) " +
                         $"select {testItem.Id}, '{testItem.Name}', '{testItem.OrderDate}', {testItem.ItemPrice}, {(testItem.ItemInStock?"1":"0")}, " +
                         $"'{testItem.ItemWarehouse}', {testItem.ItemRevenue}, {testItem.ItemQuantity};";
            var res = _conn.Execute(sql);
            return res;
        }
    }
}
