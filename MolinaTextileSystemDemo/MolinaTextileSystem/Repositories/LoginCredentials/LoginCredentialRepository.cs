using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.LoginCredentials
{
    public class LoginCredentialRepository : ILoginCredentialRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public LoginCredentialRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spAuthors_GetAll";

                return
                    connection.Query<EmployeeModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public IEnumerable<LoginCredentialModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storedProcedure = "spBooks_GetAll";

                var loginCredentials = connection.Query<LoginCredentialModel, EmployeeModel, LoginCredentialModel>
                    (storedProcedure, (loginCredential, employee) => {
                        loginCredential.Employee = employee;

                        return loginCredential;
                    },
                    splitOn: "EmployeeName",
                    commandType: CommandType.StoredProcedure);

                return loginCredentials;
            }
        }


        public LoginCredentialModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spBooks_GetById";

                return
                    connection.QueryFirstOrDefault<LoginCredentialModel>(
                        storeProcedure,
                        new { LoginCredentialId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(LoginCredentialModel loginCredential)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spBooks_Insert";

                connection.Execute(
                    storeProcedure,
                    new { loginCredential.EmployeeId, loginCredential.Username, loginCredential.Password },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Edit(LoginCredentialModel loginCredential)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spBooks_Update";

                connection.Execute(
                    storeProcedure,
                    new { loginCredential.LoginCredentialId, loginCredential.EmployeeId, loginCredential.Username, loginCredential.Password },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spBooks_Delete";

                connection.Execute(
                    storeProcedure,
                    new { LoginCredentialId = id },
                    commandType: CommandType.StoredProcedure
                );
            }

        }
    }
}
