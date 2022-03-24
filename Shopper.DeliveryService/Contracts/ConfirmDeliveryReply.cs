using System.Runtime.Serialization;

namespace Shopper.DeliveryService.Contracts
{
    [DataContract]
    public class ConfirmDeliveryReply
    {
        [DataMember(Order = 1)]
        public bool IsConfirmed { get; set; }
    }
}
