using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.LoginCredentials
{
    public interface ILoginCredentialRepository
    {
        void Add(LoginCredentialModel loginCredential);
        void Delete(int id);
        void Edit(LoginCredentialModel loginCredential);
        IEnumerable<LoginCredentialModel> GetAll();
        IEnumerable<EmployeeModel> GetAllEmployees();
        LoginCredentialModel? GetById(int id);
    }
}
