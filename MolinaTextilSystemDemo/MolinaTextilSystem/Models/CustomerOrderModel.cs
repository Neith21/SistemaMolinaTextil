namespace MolinaTextilSystem.Models
{
    public class CustomerOrderModel
    {
        public int CustomerOrderID { get; set; }    

        public string CustomerName { get; set; }

        public string CustomerOrderIssue { get; set; }

        public string CustomerOrderDescription { get; set; }

        public string TextileRecipe { get; set; }

        public decimal TotalAmount {  get; set; }
    }
}
