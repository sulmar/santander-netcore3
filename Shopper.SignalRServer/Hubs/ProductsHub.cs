using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shopper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.SignalRServer.Hubs
{
    public class ProductsHub : Hub
    {
        private readonly ILogger<ProductsHub> logger;

        public ProductsHub(ILogger<ProductsHub> logger)
        {
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            logger.LogInformation("Connected: {0}", this.Context.ConnectionId);

            await this.Groups.AddToGroupAsync(Context.ConnectionId, "A");

            await base.OnConnectedAsync();
        }

        public async Task SendPrice(Product product)
        {
            await this.Clients.Others.SendAsync("PriceChanged", product);

            // await this.Clients.Groups("A").SendAsync("PriceChanged", product);
        }

        public async Task Ping()
        {
            await this.Clients.Caller.SendAsync("Pong");
        }
    }
}
