using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;
using Dapper;

namespace MolinaTextileSystem.Repositories.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public SupplierRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<SupplierModel> GetAll()
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
        public SupplierModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spSupplier_GetById";

                return
                    connection.QueryFirstOrDefault<SupplierModel>(
                        storeProcedure,
                        new { SupplierId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(SupplierModel supplier)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spSupplier_Insert";

                connection.Execute(
                    storeProcedure,
                    new
                    {
                        supplier.SupplierName,
                        supplier.Manager,
                        supplier.SupplierPhone,
                        supplier.SupplierEmail
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public void Edit(SupplierModel supplier)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spSupplier_Update";

                connection.Execute(
                    storeProcedure,
                    supplier,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spSupplier_Delete";

                connection.Execute(
                    storeProcedure,
                    new { SupplierId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
