using Microsoft.Extensions.Configuration;
using Serilog;
using StarRocksClient.Db;
using StarRocksClient.Models;

namespace StarRocksClient.Core.Generators
{
    public class CustomerGenerator
    {
        #region Fields
        private string _connStr;
        private int _rows;
        private string _className = nameof(CustomerGenerator);
        private CoreDbDao _dao;
        private int _pageCount = 5;
        #endregion

        #region Constructor
        public CustomerGenerator(string connectionString, int rowsToGenerate)
        {
            _connStr = connectionString;
            _rows = rowsToGenerate;
            _dao = new CoreDbDao(connectionString);
        }
        #endregion

        #region Properties

        #endregion

        #region Methods
        public void Start() 
        {
            string methodName = "Start";
            Log.Information($"{_className}:{methodName}=> Start Generator. ");

            Log.Information($"{_className}:{methodName}=> Get Max Key From Entity If Any.");

            Customer cust = new Customer(1);

            var maxKey = _dao.GetMaxKey(cust);

            Log.Information($"{_className}:{methodName}=> Got Max Key {maxKey} From Entity.");

            int pkey = 0;

            if (maxKey != null)
            {
                pkey = (int)maxKey;
            }

            Log.Information($"{_className}:{methodName}=> Set Primary Key To {pkey}.");

            List<Customer> customers = new List<Customer>();

            for (int i = 1; i < _rows + 1; i++)
            {
                customers.Add(new Customer(pkey + i));
            }

            Log.Information($"{_className}:{methodName}=> Generated {customers.Count} Entities.");

            Log.Information($"{_className}:{methodName}=> Batch Count Is Set to {_pageCount}.");

            foreach (var custs in customers.Page(_pageCount))
            {
                try
                {
                    var res = _dao.InsertMany(custs.ToList());
                    Log.Information($"{_className}:{methodName}=> Inserted {res} Entities.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex,$"{_className}:{methodName}=> Could not Insert Entities.");
                }

            }

        }
        #endregion
    }
}
