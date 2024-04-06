namespace MolinaTextilSystem.Models
{
    public class CustomersOrdersDetailModel
    {
        public int CustomersOrdersDetailID { get; set; }
        public int CustomerOrderID { get; set; }
        public int ProductID { get; set; }
        public float UnitPrice { get; set; }
        public int Amount { get; set; }
    }
}
