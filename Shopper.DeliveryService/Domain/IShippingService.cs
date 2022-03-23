using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.DeliveryService.Domain
{
    public class ConfirmDeliveryRequest2
    {
        public int OrderId { get; set; }
        public string Sign { get; set; }
    }

    public class ConfirmDeliveryReply2
    {
        public bool IsConfirmed { get; set; }
    }

    public interface IShippingService
    {
        Task<ConfirmDeliveryReply2> ConfirmDelivery(ConfirmDeliveryRequest2 request);

    }

    public class MyShippingService : IShippingService
    {
        private readonly ILogger<MyShippingService> logger;

        public MyShippingService(ILogger<MyShippingService> logger)
        {
            this.logger = logger;
        }

        public Task<ConfirmDeliveryReply2> ConfirmDelivery(ConfirmDeliveryRequest2 request)
        {
            logger.LogInformation("Confirmed {0} {1}", request.OrderId, request.Sign);

            ConfirmDeliveryReply2 reply = new ConfirmDeliveryReply2 { IsConfirmed = true };

            return Task.FromResult(reply);
        }
    }
}
