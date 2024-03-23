namespace MolinaTextilSystem.Models
{
    public class InventoryRawMaterialsModel
    {
        public int RawMaterialID { get; set; }
        public string RawMaterialName { get; set; }
        public string RawMaterialDescription { get; set; }
        public string SupplierContact { get; set; }
        public double StockQuantity { get; set; }
        public double StockQuantityUsed { get; set; }
    }
}
