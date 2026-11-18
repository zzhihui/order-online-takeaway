namespace Takeway.Web.Models
{
    public class ChargeCalculatorViewModel
    {
        public decimal TotalSale { get; set; }
        public int NumberOfTransactions { get; set; }
        public decimal CardCharge { get; set; }
        public decimal TransferAmount { get; set; }
    }
}