namespace Takeway.Web.ViewModels.Order
{
    public class ListPaymentViewModel: ViewModelBase
    {
        public decimal? TotalAmount { get; set; }
        public decimal? OrderTotal { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public bool? IsEnoughPoint { get; set; }
        public decimal? CostCredits { get; set; }

    }
}