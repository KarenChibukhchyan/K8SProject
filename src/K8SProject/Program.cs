using Microsoft.Extensions.Configuration;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        try
        {
            for (int i = 0; i < 1; i++)
            {
                Log.Information("Hello another world!!!");
                Thread.Sleep(3_000);
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "There was a problem starting the application");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}