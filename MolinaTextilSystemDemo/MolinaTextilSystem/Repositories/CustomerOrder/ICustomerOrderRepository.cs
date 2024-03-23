using MolinaTextilSystem.Models;
namespace MolinaTextilSystem.Repositories.CustomerOrder
{
    public interface ICustomerOrderRepository
    {
        void Add(CustomerOrderModel CustomerModel);
        void Delete(int id);
        void Edit(CustomerOrderModel CustomerOrder);
        IEnumerable<CustomerOrderModel> GetAll();
        CustomerOrderModel? GetById(int id);
    }
}
