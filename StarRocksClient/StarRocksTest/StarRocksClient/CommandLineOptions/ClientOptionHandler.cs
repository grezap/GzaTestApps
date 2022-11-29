using Microsoft.Extensions.Configuration;
using StarRocksClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            StarRocksCore core = new StarRocksCore(_config);


        }
        #endregion
    }
}
