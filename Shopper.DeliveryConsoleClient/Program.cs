using Bogus;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shopper.DeliveryService;
using Shopper.DeliveryService.Domain;
using System;
using System.Threading.Tasks;

namespace Shopper.DeliveryConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // await RequestReplayTest();

            // await GetStreamTest();

            // dotnet add package Grpc.Net.Client
            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);
            var client = channel.CreateGrpcService<Shopper.DeliveryService.Domain.IShippingService>();

            var faker = new Faker<ConfirmDeliveryRequest2>()
             .RuleFor(p => p.OrderId, f => f.IndexFaker)
             .RuleFor(p => p.Sign, f => f.Lorem.Word());

            var requests = faker.GenerateForever();

            foreach (var request in requests)
            {
                Console.Write($"Send {request.OrderId} {request.Sign} Status: ");

                var response = await client.ConfirmDelivery(request);

                Console.WriteLine(response.IsConfirmed);

                await Task.Delay(TimeSpan.FromSeconds(0.1));


            }

            Console.WriteLine("Press key to exit.");
            Console.ReadKey();


        }

        private static async Task GetStreamTest()
        {
            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);

            Shopper.DeliveryService.ShippingService.ShippingServiceClient client = new DeliveryService.ShippingService.ShippingServiceClient(channel);

            var request = new GetNextLocationRequest { DriverId = 1 };

            var reply = client.GetNextLocation(request);

            var nextLocations = reply.ResponseStream.ReadAllAsync();

            await foreach (var nextLocation in nextLocations)
            {
                Console.WriteLine($"{nextLocation.CustomerName} ({nextLocation.Latitude},{nextLocation.Longitude})");
            }
        }

        private static async Task RequestReplayTest()
        {
            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);

            Shopper.DeliveryService.ShippingService.ShippingServiceClient client = new DeliveryService.ShippingService.ShippingServiceClient(channel);

            var faker = new Faker<ConfirmDeliveryRequest>()
                .RuleFor(p => p.OrderId, f => f.IndexFaker)
                .RuleFor(p => p.Sign, f => f.Lorem.Word());

            var requests = faker.GenerateForever();

            foreach (var request in requests)
            {
                Console.Write($"Send {request.OrderId} {request.Sign} Status: ");

                var response = await client.ConfirmDeliveryAsync(request);

                Console.WriteLine(response.IsConfirmed);

                await Task.Delay(TimeSpan.FromSeconds(0.1));


            }
        }
    }
}
