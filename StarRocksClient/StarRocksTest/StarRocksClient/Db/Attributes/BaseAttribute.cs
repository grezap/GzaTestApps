namespace StarRocksClient.Db.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BaseAttribute : Attribute
    {
        private string _dbCol;

        public BaseAttribute(string dbCol)
        {
            _dbCol = dbCol;
        }

        public string DbCol
        {
            get => _dbCol;
            set => _dbCol = value;
        }
    }
}
