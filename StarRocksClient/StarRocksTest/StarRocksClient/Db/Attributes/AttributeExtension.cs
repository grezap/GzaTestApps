using System.Reflection;

namespace StarRocksClient.Db.Attributes
{
    public static class AttributeExtension
    {
        public static string GetTableName(this System.Type type)
        {
            return type.GetCustomAttribute<TableName>().Name ?? String.Empty;
        }

        public static IList<string> GetSelectableProperties(this IList<PropertyInfo> properties)
        {
            var res = properties.SelectMany(p => p.GetCustomAttributes(typeof(Selectable), false)).Select(a => (Selectable)a).Select(a => a.DbCol).ToList();
            return res;
        }

        public static IList<string> GetInsertableProperties(this IList<PropertyInfo> properties)
        {
            var res = properties.SelectMany(p => p.GetCustomAttributes(typeof(Insertable), false)).Select(a => (Insertable)a).Select(a => a.DbCol).ToList();
            return res;
        }

        public static string GetPrimaryKey(this IList<PropertyInfo> properties)
        {
            var res = properties.SelectMany(p => p.GetCustomAttributes(typeof(PrimaryKey), false)).Select(a => (PrimaryKey)a).Select(a => a.DbCol).SingleOrDefault();
            return res;
        }
    }
}
