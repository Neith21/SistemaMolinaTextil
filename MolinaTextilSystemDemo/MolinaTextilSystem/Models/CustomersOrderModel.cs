namespace MolinaTextilSystem.Models
{
    public class CustomersOrderModel
    {
        public int CustomerOrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public string CreationDate { get; set; }
        public string DeliveryDate { get; set; }
        public float OrderPrice { get; set; }
        public int StateID { get; set; }
    }
}
