using MolinaTextilSystem.Models;

namespace MolinaTextilSystem.Repositories.Login
{
    public interface ILoginRepository
    {
        void AddEmployee(CredentialsModel credentials);
        void DeleteEmployee(int id);
        void EditEmployee(CredentialsModel credentials);
        IEnumerable<CredentialsModel> GetAllEmployee();
        CredentialsModel GetByUsernameEmployee(string username, string password);
        bool ConfirmLogin(CredentialsModel credentials);
    }
}
