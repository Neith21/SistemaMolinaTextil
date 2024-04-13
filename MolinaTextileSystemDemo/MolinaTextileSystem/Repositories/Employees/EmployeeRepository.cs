using Dapper;
using MolinaTextileSystem.Controllers;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public EmployeeRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_GetAll";

                return
                    connection.Query<EmployeeModel>(
                        storeProcedure,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
        public EmployeeModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_GetById";

                return
                    connection.QueryFirstOrDefault<EmployeeModel>(
                        storeProcedure,
                        new { EmployeeId = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Add(EmployeeModel employee)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_Insert";

                connection.Execute(
                    storeProcedure,
                    new { employee.EmployeeName, employee.EmployeeLastname, employee.EmployeeAddress, 
                          employee.EmployeePhone, employee.EmployeeEmail },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public void Edit(EmployeeModel employee)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_Update";

                connection.Execute(
                    storeProcedure,
                    employee,
                    commandType: CommandType.StoredProcedure
                );
            }
        }   

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spEmployee_Delete";

                connection.Execute(
                    storeProcedure,
                    new { EmployeeId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
        }


    }
}
