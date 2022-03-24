using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using Shopper.DeliveryService.Contracts;
using System.Threading.Tasks;

namespace Shopper.DeliveryService.Services
{
    public class MyShippingService : IShippingService
    {
        private readonly ILogger<MyShippingService> logger;

        public MyShippingService(ILogger<MyShippingService> logger)
        {
            this.logger = logger;
        }

        public Task<Contracts.ConfirmDeliveryReply> ConfirmDelivery(Contracts.ConfirmDeliveryRequest request, CallContext context = default)
        {
            logger.LogInformation("Confirmed {0} {1}", request.OrderId, request.Sign);

            Contracts.ConfirmDeliveryReply reply = new Contracts.ConfirmDeliveryReply { IsConfirmed = true };

            return Task.FromResult(reply);
        }
    }
}
