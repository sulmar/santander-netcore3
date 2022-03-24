using System.Threading.Tasks;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Shopper.DeliveryService.Contracts
{
    [ServiceContract]
    public interface IShippingService
    {
        [OperationContract]
        Task<ConfirmDeliveryReply> ConfirmDelivery(ConfirmDeliveryRequest request, CallContext context = default);

    }
}
