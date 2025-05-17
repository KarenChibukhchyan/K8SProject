using Microsoft.Extensions.Configuration;
using Serilog;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

try
{
    Log.Information("Hello world?!");
}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a problem starting the application");
}
finally
{
    Log.CloseAndFlush();
}