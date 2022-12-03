using Microsoft.Extensions.Configuration;
using StarRocksClient.Core;

namespace StarRocksClient.CommandLineOptions
{
    public class ClientOptionHandler
    {
        #region Fields
        private IConfiguration _config;
        #endregion

        #region Constructor
        public ClientOptionHandler(IConfiguration configuration)
        {
            _config = configuration;
        }
        #endregion

        #region Methods
        public void HandleOptions(ClientOption options) 
        {
            StarRocksCore core = new StarRocksCore(_config, options);

            if (options.GenerateAll)
            {
                core.AllGenerator();
                return;
            }
            if (options.Model == Enums.ModelGeneratorEnum.Customer)
            {
                core.CustomerGeneration();
            }
            if (options.Model == Enums.ModelGeneratorEnum.Product)
            {
                core.ProductGeneration();
            }
            if (options.Model == Enums.ModelGeneratorEnum.Order)
            {
                core.OrderGeneration();
            }
            if (options.Model == Enums.ModelGeneratorEnum.Supplier)
            {
                core.SupplierGeneration();
            }
            if (options.Model == Enums.ModelGeneratorEnum.Lineitem)
            {
                core.OrderItemGeneration();
            }
        }
        #endregion
    }
}
