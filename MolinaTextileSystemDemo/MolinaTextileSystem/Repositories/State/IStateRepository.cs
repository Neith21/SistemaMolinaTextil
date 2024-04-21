using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Estado
{
    public interface IStateRepository
    {
        void Add(StateModel state);
        void Delete(int id);
        void Edit(StateModel state);
        IEnumerable<StateModel> GetAll();
        StateModel? GetById(int id);
    }
}