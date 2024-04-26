using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Pattern
{
    public interface IPatternRepository
    {
        void Add(PatternModel pattern);
        void Delete(int id);
        void Edit(PatternModel pattern);
        IEnumerable<PatternModel> GetAll();
        PatternModel? GetById(int id);
    }
}