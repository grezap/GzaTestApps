// See https://aka.ms/new-console-template for more information
using StarRocksClient.Core;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using StarRocksClient.CommandLineOptions;
using CommandLine;
using Google.Protobuf.WellKnownTypes;
using Serilog;

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

        #region Log Configuration
        string logDir = _configuration.GetSection("Logger").GetSection("Directory").Value.ToString();
        Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console(
                            outputTemplate: "[{Timestamp:o} {SourceContext} [{Level}] {Message}{NewLine}{Exception}",
                            theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code //"Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console"
                        )
                        .WriteTo.File(
                            $"{logDir}\\\\log_{DateTime.Now.ToString("yyyy_MM_dd")}.txt",
                            outputTemplate: "[{Timestamp:o} {SourceContext} [{Level}] {Message}{NewLine}{Exception}",
                            fileSizeLimitBytes: 10485760,
                            rollOnFileSizeLimit: true,
                            flushToDiskInterval: TimeSpan.FromSeconds(20),
                            retainedFileCountLimit: 1000,
                            rollingInterval: RollingInterval.Day
                    ).CreateLogger();
        #endregion

        ClientOptionHandler handler = new ClientOptionHandler(_configuration);
        try
        {
            var res = Parser.Default
            .ParseArguments<ClientOption>(args)
            .WithParsed<ClientOption>(opts => handler.HandleOptions(opts))
            ;
            Log.CloseAndFlush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could Not Parse The Arguments Provided. Exiting Application. {ex.Message}");
            Log.CloseAndFlush();
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