// See https://aka.ms/new-console-template for more information
using StarRocksClient.Core;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using StarRocksClient.CommandLineOptions;
using CommandLine;

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

        ClientOptionHandler handler = new ClientOptionHandler(_configuration);
        try
        {
            var res = Parser.Default
            .ParseArguments<ClientOption>(args)
            .WithParsed<ClientOption>(opts => handler.HandleOptions(opts))
            ;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could Not Parse The Arguments Provided. Exiting Application. {ex.Message}");
        }
        //StarRocksCore core = new StarRocksCore();
        //core.Start();
        //Console.ReadLine();
    }

    private static void ErrorsHandler(IEnumerable<Error> errors)
    {
        foreach (var err in errors)
        {
            Console.WriteLine($"Error: {err.Tag.ToString()}");
            //Log.Information($"Error: {err.Tag.ToString()}");
        }
    }
}