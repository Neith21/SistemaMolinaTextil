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
        IEnumerable<RolModel> GetAllRoles();
        LoginCredentialModel? GetById(int id);
        LoginCredentialModel? GetCredentials(string username, string password);
    }
}
