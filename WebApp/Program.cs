using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(hostBuilderContext =>
                {
                    var endpointConfiguration = new EndpointConfiguration("SamplesFinance");
                    endpointConfiguration.SendFailedMessagesTo("SamplesFinanceErrors");
                    endpointConfiguration.EnableInstallers();

                    endpointConfiguration.UsePersistence<LearningPersistence>();


                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    transport.ConnectionString("host=localhost;username=guest;password=guest");
                    transport.UseConventionalRoutingTopology();
                    //transport.Routing().RouteToEndpoint(
                    //    assembly: typeof(MyMessage).Assembly,
                    //    destination: "Samples.ASPNETCore.Endpoint");

                    //endpointConfiguration.SendOnly();

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
