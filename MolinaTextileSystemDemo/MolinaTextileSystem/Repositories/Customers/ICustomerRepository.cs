using MolinaTextileSystem.Controllers;
using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Employees
{
    public interface ICustomerRepository
    {
        void Add(CustomerModel customer);
        void Delete(int id);
        void Edit(CustomerModel customer);
        IEnumerable<CustomerModel> GetAll();
        CustomerModel? GetById(int id);
    }
}
