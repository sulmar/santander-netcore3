using Microsoft.AspNetCore.SignalR.Client;
using Shopper.Domain;
using System;
using System.Threading.Tasks;

namespace Shopper.Receiver.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Receiver Signal-R!");

            const string url = "https://localhost:5001/signalr/products";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<Product>("PriceChanged",
                product => Console.WriteLine($"{product.Name} {product.Price}"));

            Console.WriteLine($"Connecting to {connection}");
            await connection.StartAsync();
            Console.WriteLine($"Connected. {connection.ConnectionId}");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
