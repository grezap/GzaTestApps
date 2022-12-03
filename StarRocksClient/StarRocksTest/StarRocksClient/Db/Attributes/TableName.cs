namespace StarRocksClient.Db.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TableName : Attribute
    {
        private string _name;
        public TableName(string name)
        {
            _name = name;
        }
        public string Name { get => _name; set => _name = value; }
    }
}
