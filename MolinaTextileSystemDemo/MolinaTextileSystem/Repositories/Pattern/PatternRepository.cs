using Dapper;
using MolinaTextileSystem.Data;
using MolinaTextileSystem.Models;
using System.Data;

namespace MolinaTextileSystem.Repositories.Pattern
{
    public class PatternRepository : IPatternRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public PatternRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<PatternModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spPattern_GetAll";

                return connection.Query<PatternModel>(
                                        storeProcedure,
                                        commandType: CommandType.StoredProcedure
                                        );
            }
        }

        public PatternModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spPattern_GetById";

                return connection.QueryFirstOrDefault<PatternModel>(
                                    storeProcedure,
                                    new { PatternId = id },
                                    commandType: CommandType.StoredProcedure
                                   );
            }
        }

        public void Add(PatternModel pattern)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spPattern_Insert";

                connection.Execute(
                    storeProcedure,
                    new { pattern.PatternName, pattern.PatternDescription },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Edit(PatternModel pattern)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spPattern_Update";

                connection.Execute(
                        storeProcedure,
                        pattern,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spPattern_Delete";

                connection.Execute(
                    storeProcedure,
                    new { PatternId = id },
                    commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
