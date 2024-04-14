using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        void Add(EmployeeModel employee);
        void Delete(int id);
        void Edit(EmployeeModel employee);
        IEnumerable<EmployeeModel> GetAll();
        EmployeeModel? GetById(int id);
    }
}
