using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.Estado
{
    public class StateRepository : IStateRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public StateRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<StateModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_GetAll";

                return connection.Query<StateModel>(
                                        storeProcedure,
                                        commandType: CommandType.StoredProcedure
                                        );
            }
        }

        public StateModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_GetById";

                return connection.QueryFirstOrDefault<StateModel>(
                                    storeProcedure,
                                    new { StateId = id },
                                    commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(StateModel state)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_Insert";

                connection.Execute(
                    storeProcedure,
                    new { StateName = state.StateName, StateDescription = state.StateDescription },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Edit(StateModel state)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_Update";

                connection.Execute(
                        storeProcedure,
                        state,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spStates_Delete";

                connection.Execute(
                    storeProcedure,
                    new { StateId = id },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

    }
}
