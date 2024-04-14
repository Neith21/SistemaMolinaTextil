using Dapper;
using MolinaTextileSystem.Controllers;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.Employees
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISqlDataAccess _dataAccess;


        public CustomerRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_GetAll";

                return connection.Query<CustomerModel>(
                                        storeProcedure,
                                        commandType: CommandType.StoredProcedure
                                        );
            }
        }

        public CustomerModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_GetById";

                return connection.QueryFirstOrDefault<CustomerModel>(
                                    storeProcedure,
                                    new { CustomerId = id },
                                    commandType: CommandType.StoredProcedure
                                   );
            }
        }

        public void Add(CustomerModel customer)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_Insert";

                connection.Execute(
                    storeProcedure,
                    new { customer.CustomerName, customer.CustomerAddress, customer.CustomerPhone },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Edit(CustomerModel customer)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_Update";

                connection.Execute(
                        storeProcedure,
                        customer,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_Delete";

                connection.Execute(
                    storeProcedure,
                    new { CustomerId = id },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
