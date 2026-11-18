namespace Takeaway.Service.Contract.Entities.Shop
{
    public class WsDeliveryOption : WsEntity
    {
        public string Name { get; set; }

        public bool? IsInternal { get; set; }
       
    }
}