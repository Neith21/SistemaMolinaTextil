using System.Data;

namespace MolinaTextileSystem.Data
{
    public interface ISqlDataAccess
    {
        IDbConnection GetConnection();
    }
}
