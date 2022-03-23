using Bogus;
using Grpc.Net.Client;
using Shopper.DeliveryService;
using System;
using System.Threading.Tasks;

namespace Shopper.DeliveryConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);

            Shopper.DeliveryService.ShippingService.ShippingServiceClient client = new DeliveryService.ShippingService.ShippingServiceClient(channel);

            var faker = new Faker<ConfirmDeliveryRequest>()
                .RuleFor(p => p.OrderId, f => f.IndexFaker)
                .RuleFor(p => p.Sign, f => f.Lorem.Word());

            var requests = faker.GenerateForever();

            foreach (var request in requests)
            {
                Console.Write($"Send {request.OrderId} {request.Sign} Status: " );

                var response = await client.ConfirmDeliveryAsync(request);

                Console.WriteLine(response.IsConfirmed);

                await Task.Delay(TimeSpan.FromSeconds(0.1));

                
            }

            
        }
    }
}
