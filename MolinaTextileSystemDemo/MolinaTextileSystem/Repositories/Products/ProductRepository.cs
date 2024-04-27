using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;
using Dapper;

namespace MolinaTextileSystem.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public ProductRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<PatternModel> GetAllPattern()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spPattern_GetAll";

                return
                    connection.Query<PatternModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<StateModel> GetAllState()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spStates_GetAll";

                return
                    connection.Query<StateModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<ProductModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spProduct_GetAll";

                var product = connection.Query<ProductModel, PatternModel, StateModel, ProductModel>
                    (storedProcedure, (product, pattern, states) => {
                        product.Pattern = pattern;
                        product.States = states;

                        return product;
                    },
                    splitOn: "PatternName, StateName",
                    commandType: CommandType.StoredProcedure);

                return product;
            }
        }


        public ProductModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spProduct_GetById";

                return
                    connection.QueryFirstOrDefault<ProductModel>(
                        storeProcedure,
                        new { ProductId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(ProductModel product)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spProduct_Insert";

                connection.Execute(
                    storeProcedure,
                    new { product.ProductName, product.ProductSize, product.PatternId, product.StateId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Edit(ProductModel product)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spProduct_Update";

                connection.Execute(
                    storeProcedure,
                    new { product.ProductId, product.ProductName, product.ProductSize, product.PatternId, product.StateId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spProduct_Delete";

                connection.Execute(
                    storeProcedure,
                    new { ProductId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
