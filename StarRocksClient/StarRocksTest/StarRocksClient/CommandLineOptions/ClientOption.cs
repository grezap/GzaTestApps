using CommandLine;
using StarRocksClient.Enums;

namespace StarRocksClient.CommandLineOptions
{
    [Verb("Generate",HelpText = "Data Generator")]
    public class ClientOption
    {
        [Option("all", HelpText = "Do Not Check For Specific chosen entity and just generate all the required dataset starting with customer.")]
        public bool GenerateAll { get; set; }

        [Option("model", HelpText = "Model To Generate", Default = ModelGeneratorEnum.Customer)]
        public ModelGeneratorEnum Model { get; set; }

        [Option("rowsNumber", HelpText = "Number Of Rows To Generate",Default = 100)]
        public int RowsNumber { get; set; }
    }
}
