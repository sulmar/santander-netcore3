using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Shopper.Domain;
using Bogus;
using Shopper.Infrastructure.Fakers;
using System.Threading;

namespace Shopper.Sender.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Sender Signal-R!");

            const string url = "https://localhost:5001/signalr/products";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;

            Console.WriteLine($"Connecting to {connection}");
            await connection.StartAsync();
            Console.WriteLine($"Connected. {connection.ConnectionId}");

            Faker<Product> faker = new ProductFaker();

            var products = faker.GenerateForever();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            // cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));

            CancellationToken cancellationToken = cancellationTokenSource.Token;

            IProgress<Product> progress = new Progress<Product>(
                product => Console.WriteLine($"Send {product.Name}"));
           

            foreach(var product in products)
            {
                cancellationToken.ThrowIfCancellationRequested();

                //if (cancellationToken.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException();
                //}

                // Console.WriteLine($"Send {product.Name} {product.Price}");

                progress.Report(product);

                await connection.SendAsync("SendPrice", product, cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                
                // cancellationTokenSource.Cancel();
            }


            Console.WriteLine("Finished.");
        }

        private static Task Connection_Reconnected(string arg)
        {
            Console.WriteLine("Reconnected");

            return Task.CompletedTask;
        }

        private static Task Connection_Reconnecting(Exception arg)
        {
            Console.WriteLine("Reconnecting...");

            return Task.CompletedTask;
        }


    }
}
