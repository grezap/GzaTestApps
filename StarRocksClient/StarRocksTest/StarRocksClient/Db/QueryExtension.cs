using StarRocksClient.Db.Attributes;
using System.Globalization;
using System.Reflection;

namespace StarRocksClient.Db
{
    public static class QueryExtension
    {

        public static string GetSelectQuery<T>(this T entity, string where = null, string orderBy = null, string limit = null)
        {
            string tableName = entity.GetType().GetTableName();
            IList<PropertyInfo> properties = entity.GetProperties();

            string sql = "SELECT ";
            
            string columns = properties.GetSelectableProperties().GetSelectSeparatedColumns(",");
            
            sql += columns + $" FROM {tableName} ";
            if (!String.IsNullOrEmpty(where))
            {
                sql += $"WHERE {where} ";
            }
            if (!String.IsNullOrEmpty(orderBy))
            {
                sql += $"ORDER BY {orderBy} ";
            }
            if (!String.IsNullOrEmpty(limit))
            {
                sql += $"LIMIT {limit} ";
            }
            return sql;
        }

        public static (string sql, IList<PropertyMapper> propMappers) GetInsertBatchValues<T>(this T entity)
        {
            IList<PropertyInfo> properties = entity.GetProperties();

            string sql = "";

            var propMappers = properties.GetInsertablePropertyMappers(entity);
            sql = string.Join(", ", propMappers.OrderBy(pm => pm.PropertyIndex).Select(pm => pm.SqlValue));

            if (sql.TrimEnd().EndsWith(",")) sql = sql.Remove(sql.LastIndexOf(","), 1);

            return ($"({sql})", propMappers);
        }

        public static IList<PropertyMapper> GetInsertablePropertyMappers<T>(this IList<PropertyInfo> properties, T entity, int index = 0)
        {
            IList<PropertyMapper> res = new List<PropertyMapper>();
            foreach (PropertyInfo p in properties)
            {
                if (p.GetCustomAttribute(typeof(Insertable), false) == null) continue;
                PropertyMapper mapper = new PropertyMapper();
                mapper.PropertyIndex = index++;
                mapper.PropertyName = p.Name;
                mapper.PropertyValue = p.GetValue(entity);
                mapper.PropertyAttributeName = ((Insertable)p.GetCustomAttribute(typeof(Insertable), false)).DbCol;
                mapper.SqlValue = mapper.PropertyValue.GetSqlStringValue();
                res.Add(mapper);
            }
            return res;
        }

        public static string GetSqlStringValue(this object prop)
        {
            string sqlNull = "NULL";

            if (prop is null) return sqlNull;
            if (prop is long) return $"{(long)prop}";
            if (prop is long?) return prop == null ? sqlNull : $"{(long?)prop}";
            if (prop is int) return $"{(int)prop}";
            if (prop is int?) return prop == null ? sqlNull : $"{(int?)prop}";
            if (prop is decimal) return $"{((decimal)prop).ToString(CultureInfo.InvariantCulture)}";
            if (prop is decimal?) return prop == null ? sqlNull : $"{((decimal)prop).ToString(CultureInfo.InvariantCulture)}";
            if (prop is double) return $"{((double)prop).ToString(CultureInfo.InvariantCulture)}";
            if (prop is double?) return prop == null ? sqlNull : $"{((double)prop).ToString(CultureInfo.InvariantCulture)}";
            if (prop is bool) return (bool)prop == false ? "0" : "1";
            if (prop is bool?) return prop == null ? sqlNull : ((bool)prop == false ? "0" : "1");
            if (prop is string) return prop == null ? sqlNull : $@"'{((string)prop).Replace("'", "''").Replace("\\", "\\\\")}'";
            if (prop is DateTime) return $"'{((DateTime)prop).ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
            if (prop is DateTime?) return prop == null ? sqlNull : $"'{((DateTime?)prop)?.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";

            return prop.ToString();
        }

        public static string GetInsertBatchQuery<T>(this T entity, IList<PropertyMapper> propMappers) 
        {
            string tableName = entity.GetType().GetTableName();
            string sql = $"INSERT INTO {tableName} (";

            string columns = propMappers.OrderBy(pm => pm.PropertyIndex).Select(pm => pm.PropertyAttributeName).ToList().GetSelectSeparatedColumns(",");

            sql += columns + " ) VALUES ";

            return sql;
        }

    }
}
