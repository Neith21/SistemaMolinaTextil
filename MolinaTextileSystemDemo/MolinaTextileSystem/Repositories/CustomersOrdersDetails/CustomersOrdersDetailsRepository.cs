using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.CustomersOrdersDetails
{
    public class CustomersOrdersDetailsRepository : ICustomersOrdersDetailsRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public CustomersOrdersDetailsRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<ProductModel> GetAllProduct()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spProduct_GetAll";

                return
                    connection.Query<ProductModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<CustomerOrderModel> GetAllCustomerOrder()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomerOrders_GetAll";

                return
                    connection.Query<CustomerOrderModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<CustomerOrderDetailModel> GetSpecificById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spCustomersOrdersDetails_GetAllSpecific";

                var customersOrdersDetails = connection.Query<CustomerOrderDetailModel, ProductModel, CustomerOrderDetailModel>
                    (storedProcedure, (customerOrderDetail, product) =>
                    {
                        customerOrderDetail.Producto = product;

                        return customerOrderDetail;
                    },
                    splitOn: "ProductName",
                    commandType: CommandType.StoredProcedure,
                    param: new { CustomerOrderId = id }
                );

                return customersOrdersDetails.Where(detail => detail.CustomerOrderId == id);
            }
        }

        public IEnumerable<CustomerOrderDetailModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spCustomersOrdersDetails_GetAll";

                var customersOrdersDetails = connection.Query<CustomerOrderDetailModel, ProductModel, CustomerOrderDetailModel>
                    (storedProcedure, (customerOrderDetail, product) => {
                        customerOrderDetail.Producto = product;

                        return customerOrderDetail;
                    },
                    splitOn: "ProductName",
                    commandType: CommandType.StoredProcedure);

                return customersOrdersDetails;
            }
        }

        public CustomerOrderDetailModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomersOrdersDetails_GetById";

                return
                    connection.QueryFirstOrDefault<CustomerOrderDetailModel>(
                        storeProcedure,
                        new { CustomerOrderDetailId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(CustomerOrderDetailModel customerOrderDetailModel)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomersOrdersDetails_Insert";

                connection.Execute(
                    storeProcedure,
                    new { customerOrderDetailModel.UnitPrice, 
                          customerOrderDetailModel.CustomerOrderDetailQuantity, 
                          customerOrderDetailModel.CustomerOrderId, 
                          customerOrderDetailModel.ProductId 
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public void Edit(CustomerOrderDetailModel customerOrderDetailModel)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomersOrdersDetails_Update";

                connection.Execute(
                    storeProcedure,
                    new {
                        customerOrderDetailModel.CustomerOrderDetailId,
                        customerOrderDetailModel.UnitPrice,
                        customerOrderDetailModel.CustomerOrderDetailQuantity,
                        customerOrderDetailModel.CustomerOrderId,
                        customerOrderDetailModel.ProductId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spCustomersOrdersDetails_Delete";

                connection.Execute(
                    storeProcedure,
                    new { CustomerOrderDetailId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
