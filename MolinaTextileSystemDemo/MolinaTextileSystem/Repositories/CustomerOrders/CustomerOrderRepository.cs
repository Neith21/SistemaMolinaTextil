using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.CustomerOrders
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public CustomerOrderRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spCustomers_GetAll";

                return
                    connection.Query<CustomerModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_GetAll";

                return
                    connection.Query<EmployeeModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<StateModel> GetAllStates()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_GetAll";

                return
                    connection.Query<StateModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<CustomerOrderModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spCustomerOrders_GetAll";

                var customerOrders = connection.Query<CustomerOrderModel, CustomerModel, EmployeeModel, StateModel, CustomerOrderModel>
                    (storedProcedure, (customerOrder, customer, employee, state) => {
                        customerOrder.Customer = customer;
                        customerOrder.Employee = employee;
                        customerOrder.State = state;

                        return customerOrder;
                    },
                    splitOn: "CustomerName,EmployeeName,StateName",
                    commandType: CommandType.StoredProcedure);

                return customerOrders;
            }
        }


        public CustomerOrderModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomerOrders_GetById";

                return
                    connection.QueryFirstOrDefault<CustomerOrderModel>(
                        storeProcedure,
                        new { CustomerOrderId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(CustomerOrderModel customerOrder)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomerOrders_Insert";

                connection.Execute(
                    storeProcedure,
                    new { customerOrder.CreationDate, customerOrder.DeliveryDate, customerOrder.TotalAmount, customerOrder.CustomerId, customerOrder.EmployeeId, customerOrder.StateId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Edit(CustomerOrderModel customerOrder)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomerOrders_Update";

                connection.Execute(
                    storeProcedure,
                    new { customerOrder.CustomerOrderId, customerOrder.CreationDate, customerOrder.DeliveryDate, customerOrder.TotalAmount, customerOrder.CustomerId, customerOrder.EmployeeId, customerOrder.StateId },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomerOrders_Delete";

                connection.Execute(
                    storeProcedure,
                    new { CustomerOrderId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
