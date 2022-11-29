// See https://aka.ms/new-console-template for more information
using StarRocksClient.Core;
using System.Reflection;
using Microsoft.Extensions.Configuration;

internal class Program
{
    #region Fields
    static IConfiguration _configuration;
    #endregion

    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            ;

        _configuration = builder.Build();



        StarRocksCore core = new StarRocksCore();
        core.Start();
        Console.ReadLine();
    }
}