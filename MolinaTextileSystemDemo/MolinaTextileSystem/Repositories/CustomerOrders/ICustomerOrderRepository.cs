using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.CustomerOrders
{
    public interface ICustomerOrderRepository
    {
        int Add(CustomerOrderModel customerOrder);
        void Delete(int id);
        void Edit(CustomerOrderModel customerOrder);
        IEnumerable<CustomerOrderModel> GetAll();
        IEnumerable<CustomerModel> GetAllCustomers();
        IEnumerable<EmployeeModel> GetAllEmployees();
        IEnumerable<StateModel> GetAllStates();
        CustomerOrderModel? GetById(int id);
    }
}
