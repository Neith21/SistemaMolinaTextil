using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.Category
{
    public class Categoryrepository : ICategoryRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public Categoryrepository(ISqlDataAccess sqlDataAccess)
        {
            _dataAccess = sqlDataAccess;
        }

        public IEnumerable<CategoryModel> GetAll()
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

        public CategoryModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCategory_GetById";

                return
                    connection.QueryFirstOrDefault<CategoryModel>(
                        storeProcedure,
                        new { CategoryId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(CategoryModel categoryModel)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCategory_Insert";

                connection.Execute(
                    storeProcedure,
                    new
                    {
                        categoryModel.CategoryName,
                        categoryModel.CategoryDescription,
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Edit(CategoryModel categoryModel)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCategory_Update";

                connection.Execute(
                    storeProcedure,
                    categoryModel,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCategory_Delete";

                connection.Execute(
                    storeProcedure,
                    new { CategoryId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
