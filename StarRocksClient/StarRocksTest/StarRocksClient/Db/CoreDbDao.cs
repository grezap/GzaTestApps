using StarRocksClient.Db.Attributes;
using System.Reflection;

namespace StarRocksClient.Db
{
    public class CoreDbDao
    {
        #region Fields
        private static CoreDb _coreDb;
        #endregion

        #region Constructor
        public CoreDbDao(string connString)
        {
            _coreDb = new CoreDb(connString);
        }
        #endregion

        #region Methods

        #region Generic
        public int? GetMaxKey<T>(T entity) 
        {
            try
            {
                IList<PropertyInfo> properties = entity.GetProperties();
                string table = entity.GetType().GetTableName();
                string pkey = properties.GetPrimaryKey();
                string sql = $"SELECT MAX({pkey}) FROM {table}";
                var res = _coreDb.GetScalar(sql);
                return res == null ? null : Convert.ToInt32(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertMany<T>(IList<T> entities) 
        {
            try
            {
                var res = _coreDb.InsertEntitiesBatch(entities);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Customer

        #endregion

        #endregion
    }
}
