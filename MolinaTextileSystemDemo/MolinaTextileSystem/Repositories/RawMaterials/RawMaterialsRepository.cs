using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.RawMeterials;
using System.Data;

namespace MolinaTextileSystem.Repositories.RawMaterials
{
    public class RawMaterialsRepository : IRawMaterialsRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public RawMaterialsRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCategory_GetAll";

                return
                    connection.Query<CategoryModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<SupplierModel> GetAllSuppliers()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spSupplier_GetAll";

                return
                    connection.Query<SupplierModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
        public IEnumerable<RawMaterialsModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spRawMaterial_GetAll";

                var rawMaterials = connection.Query<RawMaterialsModel, CategoryModel, SupplierModel, RawMaterialsModel>
                    (storedProcedure, (rawMaterials, category, supplier) => {
                        rawMaterials.Category = category;
                        rawMaterials.Supplier = supplier;

                        return rawMaterials;
                    },
                    splitOn: "CategoryName, SupplierName",
                    commandType: CommandType.StoredProcedure);

                return rawMaterials;
            }
        }

        public RawMaterialsModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spRawMaterial_GetById";

                return
                    connection.QueryFirstOrDefault<RawMaterialsModel>(
                        storeProcedure,
                        new { RawMaterialId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(RawMaterialsModel rawMaterials)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spRawMaterial_Insert";

                connection.Execute(
                    storeProcedure,
                    new { rawMaterials.RawMaterialName, rawMaterials.RawMaterialDescription, rawMaterials.RawMaterialPurchasePrice, rawMaterials.RawMaterialQuantity, rawMaterials.CategoryId, rawMaterials.SupplierId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Edit(RawMaterialsModel rawMaterials)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spRawMaterial_Update";

                connection.Execute(
                    storeProcedure,
                    new { rawMaterials.RawMaterialId, rawMaterials.RawMaterialName, rawMaterials.RawMaterialDescription, rawMaterials.RawMaterialPurchasePrice, rawMaterials.RawMaterialQuantity, rawMaterials.CategoryId, rawMaterials.SupplierId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spRawMaterial_Delete";

                connection.Execute(
                    storeProcedure,
                    new { RawMaterialId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
