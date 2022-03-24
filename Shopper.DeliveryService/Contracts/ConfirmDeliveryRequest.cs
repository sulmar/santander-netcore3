using System.Runtime.Serialization;

namespace Shopper.DeliveryService.Contracts
{
    [DataContract]
    public class ConfirmDeliveryRequest
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public string Sign { get; set; }
    }
}
