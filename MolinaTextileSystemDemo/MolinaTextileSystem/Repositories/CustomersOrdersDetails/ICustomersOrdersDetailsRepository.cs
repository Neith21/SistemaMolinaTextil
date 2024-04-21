using Humanizer;
using Microsoft.CodeAnalysis;
using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.CustomersOrdersDetails
{
    public interface ICustomersOrdersDetailsRepository
    {
        void Add(CustomerOrderDetailModel customerOrderDetailModel);
        void Delete(int id);
        void Edit(CustomerOrderDetailModel customerOrderDetailModel);
        IEnumerable<CustomerOrderDetailModel> GetAll();
        IEnumerable<ProductModel> GetAllProduct();
        IEnumerable<CustomerOrderModel> GetAllCustomerOrder();
        CustomerOrderDetailModel? GetById(int id);
    }
}
