using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Employees
{
    public interface ICustomerRepository
    {
        void Add(CustomerModel customerModel);
        void Delete(int id);
        void Edit(CustomerModel customerModel);
        IEnumerable<CustomerModel> GetAll();
        CustomerModel? GetById(int id);
    }
}
