using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.DeliveryService.Services
{
    public class ShippingService : Shopper.DeliveryService.ShippingService.ShippingServiceBase
    {
        private readonly ILogger<ShippingService> logger;

        public ShippingService(ILogger<ShippingService> logger)
        {
            this.logger = logger;
        }

        public override Task<ConfirmDeliveryReply> ConfirmDelivery(ConfirmDeliveryRequest request, ServerCallContext context)
        {
            logger.LogInformation("Confirmed {0} {1}", request.OrderId, request.Sign);

            ConfirmDeliveryReply reply = new ConfirmDeliveryReply { IsConfirmed = true };

            return Task.FromResult(reply);
            
        }

        public override async Task GetNextLocation(GetNextLocationRequest request, IServerStreamWriter<GetNextLocationReply> responseStream, ServerCallContext context)
        {
            var faker = new Faker<GetNextLocationReply>()
                .RuleFor(p => p.CustomerName, f => f.Company.CompanyName())
                .RuleFor(p => p.Longitude, f => (float) f.Address.Longitude())
                .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude());

            var nextLocations = faker.GenerateForever();

            
            foreach (var nextLocation in nextLocations)
            {
                logger.LogInformation($"{nextLocation.CustomerName} ({nextLocation.Latitude},{nextLocation.Longitude})");

                await responseStream.WriteAsync(nextLocation);
                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
        }
    }
}
