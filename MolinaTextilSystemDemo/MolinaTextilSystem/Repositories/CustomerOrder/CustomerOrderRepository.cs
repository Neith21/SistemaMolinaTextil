using Dapper;
using MolinaTextilSystem.Data;
using MolinaTextilSystem.Models;

namespace MolinaTextilSystem.Repositories.CustomerOrder
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public CustomerOrderRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<CustomerOrderModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "SELECT CustomerOrderID, CustomerName, CustomerOrderIssue, CustomerOrderDescription, TextileRecipe, TotalAmount FROM CustomerOrder";

                return connection.Query<CustomerOrderModel>(query);
            }
        }

        public CustomerOrderModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"SELECT CustomerOrderID, CustomerName, CustomerOrderIssue, CustomerOrderDescription, TextileRecipe, TotalAmount FROM CustomerOrder WHERE CustomerOrderID = @CustomerOrderID";

                return connection.QueryFirstOrDefault<CustomerOrderModel>(query, new { CustomerOrderID = id });
            }
        }

        public void Add(CustomerOrderModel CustomerOrder)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "INSERT INTO CustomerOrder VALUES(@CustomerName, @CustomerOrderIssue, @CustomerOrderDescription, @TextileRecipe, @TotalAmount)";

                connection.Execute(query, new { CustomerOrder.CustomerName, CustomerOrder.CustomerOrderIssue, CustomerOrder.CustomerOrderDescription, CustomerOrder.TextileRecipe, CustomerOrder.TotalAmount});
            }
        }

        public void Edit(CustomerOrderModel CustomerOrder)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"UPDATE CustomerOrder SET CustomerName = @CustomerName, CustomerOrderIssue = @CustomerOrderIssue, CustomerOrderDescription = @CustomerOrderDescription, TextileRecipe = @TextileRecipe, TotalAmount = @TotalAmount WHERE CustomerOrderID = @CustomerOrderID";

                connection.Execute(query, CustomerOrder);
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = "DELETE FROM CustomerOrder WHERE CustomerOrderID = @CustomerOrderID";

                connection.Execute(query, new { CustomerOrderID = id });
            }
        }
    }
}

