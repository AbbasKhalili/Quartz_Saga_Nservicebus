using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace WebAppPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(hostBuilderContext =>
                {
                    var endpointConfiguration = new EndpointConfiguration("SamplesPayment");
                    endpointConfiguration.SendFailedMessagesTo("SamplesPaymentErrors");
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
