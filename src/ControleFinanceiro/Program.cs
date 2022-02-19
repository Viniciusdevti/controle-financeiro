using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ControleFinanceiro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>


            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hosting, config) =>
            {
                var configurarionRoot = config.Build();

                Serilog.Log.Logger = new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .WriteTo.MSSqlServer(configurarionRoot.GetConnectionString("SERILOGS"),
                       sinkOptions: new MSSqlServerSinkOptions
                       {
                           AutoCreateSqlTable = true,
                           TableName = "Logs"
                       }).CreateLogger();
            }).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
