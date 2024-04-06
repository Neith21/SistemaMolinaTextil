namespace MolinaTextilSystem.Models
{
    public class MaterialModel
    {
        public int MaterialID { get; set; }
        public string NameMaterial { get; set; }
        public string DescriptionMaterial { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public float purchasePriceMaterial { get; set; }
        public decimal QuantityMaterial { get; set; }
    }
}
