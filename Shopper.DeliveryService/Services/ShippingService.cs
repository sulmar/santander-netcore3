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
    }
}
