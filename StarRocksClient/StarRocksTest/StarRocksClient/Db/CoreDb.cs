using Dapper;
using MySql.Data.MySqlClient;
using Serilog;
using System.Data;
using System.Text;

namespace StarRocksClient.Db
{
    public class CoreDb
    {
        #region Fields
        private string _connStr;
        #endregion

        #region Constructor
        public CoreDb(string connectionString)
        {
            _connStr = connectionString;
        }
        #endregion

        #region Methods

        #region Read
        public T GetSingle<T>(string sql, object param = null)
        {
            string methodName = "GetSingle";

            using (var conn = CreateConnection(_connStr))
            {
                try
                {
                    var res = conn.Query<T>(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public async Task<T> GetSingleAsync<T>(string sql, object param = null)
        {
            string methodName = "GetSingleAsync";

            using (var conn = await CreateConnectionAsync(_connStr))
            {
                try
                {
                    var res = await conn.QueryAsync<T>(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public IEnumerable<T> GetMany<T>(string sql, object param = null)
        {
            string methodName = "GetMany";
            using (var conn = CreateConnection(_connStr))
            {
                try
                {
                    var res = conn.Query<T>(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetManyAsync<T>(string sql, object param = null)
        {
            string methodName = "GetManyAsync";
            using (var conn = await CreateConnectionAsync(_connStr))
            {
                try
                {
                    var res = await conn.QueryAsync<T>(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public object GetScalar(string sql, object param = null)
        {
            string methodName = "GetScalar";

            using (var conn = CreateConnection(_connStr))
            {
                try
                {
                    var res = conn.ExecuteScalar(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        protected async Task<object> GetScalarAsync(string sql, object param = null)
        {
            string methodName = "GetScalarAsync";

            using (var conn = await CreateConnectionAsync(_connStr))
            {
                try
                {
                    var res = await conn.ExecuteScalarAsync(sql, param, commandTimeout: 120, commandType: CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(param == null ? "NO PARMS PROVIDED" : param.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }
        #endregion

        #region Insert
        public int InsertCustom(string sql, int? commandTimeout = 120, object parm = null) 
        {
            string methodName = "InsertCustom";
            using (var conn = CreateConnection(_connStr))
            {
                try
                {
                    //conn.Execute("SET GLOBAL max_allowed_packet=1073741824;", commandTimeout: commandTimeout, commandType: System.Data.CommandType.Text);
                    var res = conn.Execute(sql, parm, commandTimeout: commandTimeout, commandType: System.Data.CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(parm == null ? "NO PARMS PROVIDED" : parm.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public async Task<int> InsertCustomAsync(string sql, int? commandTimeout = 120, object parm = null)
        {
            string methodName = "InsertCustomAsync";
            using (var conn = await CreateConnectionAsync(_connStr))
            {
                try
                {
                    var res = await conn.ExecuteAsync(sql, parm, commandTimeout: commandTimeout, commandType: System.Data.CommandType.Text);
                    return res;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Method: {methodName}, Sql: {sql}, Parms-Values: {(parm == null ? "NO PARMS PROVIDED" : parm.GetTypePropertyNamesAndValues())}");
                    throw;
                }
            }
        }

        public async Task<int> InsertEntitiesBatchAsync<T>(IList<T> entity)
        {
            try
            {
                string sql = "";
                int i = 0;
                StringBuilder newSql = null;
                List<string> rows = new List<string>();
                foreach (var ent in entity)
                {
                    if (i == 0)
                    {
                        var result = ent.GetInsertBatchValues();
                        sql = ent.GetInsertBatchQuery(result.propMappers);
                        newSql = new StringBuilder(sql);
                        rows.Add(result.sql);
                    }
                    else
                    {
                        rows.Add(ent.GetInsertBatchValues().sql);
                    }
                    i++;
                }
                newSql.Append(String.Join(',', rows));
                newSql.Append(";");
                var res = await InsertCustomAsync(newSql.ToString());
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertEntitiesBatch<T>(IList<T> entity)
        {
            try
            {
                string sql = "";
                int i = 0;
                StringBuilder newSql = null;
                List<string> rows = new List<string>();
                foreach (var ent in entity)
                {
                    if (i == 0)
                    {
                        var result = ent.GetInsertBatchValues();
                        sql = sql + ent.GetInsertBatchQuery(result.propMappers);
                        newSql = new StringBuilder(sql);
                        rows.Add(result.sql);
                    }
                    else
                    {
                        rows.Add(ent.GetInsertBatchValues().sql);
                    }
                    i++;
                }
                newSql.Append(String.Join(',', rows));
                newSql.Append(";");
                var res = InsertCustom(newSql.ToString());
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Delete

        #endregion

        #region Update

        #endregion

        #region Connection
        private IDbConnection CreateConnection(string connStr)
        {
            var conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        private async Task<IDbConnection> CreateConnectionAsync(string connStr) 
        {
            var conn = new MySqlConnection(connStr);
            await conn.OpenAsync();
            return conn;
        }
        #endregion

        #endregion
    }
}
