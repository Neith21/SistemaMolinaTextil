using Dapper;
using MolinaTextilSystem.Data;
using MolinaTextilSystem.Models;

namespace MolinaTextilSystem.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public LoginRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void AddEmployee(CredentialsModel credentials)
        {
            throw new NotImplementedException();
        }



        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEmployee(CredentialsModel credentials)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CredentialsModel> GetAllEmployee()
        {
            throw new NotImplementedException();
            /*
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"SELECT emp.EmployeeID, emp.EmployeeName, emp.EmployeeContact, emp.EmployeeUsername, emp.EmployeePassword, ro.EmployeeRole
                                 FROM Employee emp
                                 INNER JOIN Rols ro ON emp.EmployeeRoleID =  ro.RolID";

                return connection.Query<EmployeeModel>(query);
            }
            */
        }

        public bool ConfirmLogin(CredentialsModel credentials)
        {
            throw new NotImplementedException();
        }


        public CredentialsModel GetByUsernameEmployee(string username, string password)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string query = @"SELECT emp.EmployeeUsername, emp.EmployeePassword, emp.EmployeeRolID
                                 FROM Employee emp
                                 WHERE emp.EmployeeUsername = @EmployeeName AND emp.EmployeePassword = @EmployeePassword";

                var credential = connection.QueryFirstOrDefault<CredentialsModel>(query, new { EmployeeName = username, EmployeePassword = password });


                return credential;
            }
        }
    }
}
