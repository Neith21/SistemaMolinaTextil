namespace MolinaTextileSystem.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductSize { get; set; }

        public int PatternId { get; set; }

        public int StateId { get; set; }

        public PatternModel Pattern { get; set; }

        public StateModel States { get; set; }
    }
}
