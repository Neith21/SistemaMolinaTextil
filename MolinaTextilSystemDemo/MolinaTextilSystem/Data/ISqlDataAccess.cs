using System.Data;

namespace MolinaTextilSystem.Data
{
    public interface ISqlDataAccess
    {
        IDbConnection GetConnection();
    }
}
