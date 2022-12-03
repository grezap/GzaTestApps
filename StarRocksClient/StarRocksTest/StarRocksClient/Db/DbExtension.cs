using System.Reflection;

namespace StarRocksClient.Db
{
    public static class DbExtension
    {
        public static string GetTypePropertyNamesAndValues<T>(this T type)
        {
            var props = type.GetType().GetProperties().ToList();

            var t = (from p in props
                     select new { Name = p.Name, Val = (p.GetValue(type) ?? "NULL").ToString() }
                    ).ToList()
                    ;
            string res = String.Join(", ", t);

            return res;
        }

        public static string GetSelectSeparatedColumns(this IList<string> columns, string separator)
        {
            var res = String.Join(separator, columns);
            return res;
        }

        public static IList<PropertyInfo> GetProperties<T>(this T entity)
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            return properties;
        }
    }
}
