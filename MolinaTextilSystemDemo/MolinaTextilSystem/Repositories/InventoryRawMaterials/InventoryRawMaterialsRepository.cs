using Dapper;
using MolinaTextilSystem.Data;
using MolinaTextilSystem.Models;

namespace MolinaTextilSystem.Repositories.InventoryRawMaterials
{
    public class InventoryRawMaterialsRepository : IInventoryRawMaterialsRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public InventoryRawMaterialsRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<InventoryRawMaterialsModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "SELECT RawMaterialID, RawMaterialName, RawMaterialDescription, SupplierContact, StockQuantity, StockQuantityUsed FROM InventoryRawMaterials";

                return connection.Query<InventoryRawMaterialsModel>(query);
            }
        }

        public InventoryRawMaterialsModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"SELECT RawMaterialID, RawMaterialName, RawMaterialDescription, SupplierContact, StockQuantity, StockQuantityUsed FROM InventoryRawMaterials WHERE RawMaterialID = @RawMaterialID";

                return connection.QueryFirstOrDefault<InventoryRawMaterialsModel>(query, new { Id = id });
            }
        }

        public void Add(InventoryRawMaterialsModel InventoryRawMaterials)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "INSERT INTO InventoryRawMaterials VALUES(@RawMaterialName, @RawMaterialDescription, @SupplierContact, @StockQuantity, @StockQuantityUsed)";

                connection.Execute(query, new { InventoryRawMaterials.RawMaterialName, InventoryRawMaterials.RawMaterialDescription, InventoryRawMaterials.SupplierContact, InventoryRawMaterials.StockQuantity, InventoryRawMaterials.StockQuantityUsed });
            }
        }

        public void Edit(InventoryRawMaterialsModel InventoryRawMaterials)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"UPDATE InventoryRawMaterials SET RawMaterialName = @RawMaterialName, RawMaterialDescription = @RawMaterialDescription, SupplierContact = @SupplierContact, StockQuantity = @StockQuantity, StockQuantityUsed = @StockQuantityUsed WHERE RawMaterialID = @RawMaterialID";

                connection.Execute(query, InventoryRawMaterials);
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "DELETE FROM InventoryRawMaterials WHERE RawMaterialID = @RawMaterialID";

                connection.Execute(query, new { id });
            }
        }
    }
}
