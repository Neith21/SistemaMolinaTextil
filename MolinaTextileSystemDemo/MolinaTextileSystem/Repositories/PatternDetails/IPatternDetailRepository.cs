using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.PatternDetails
{
	public interface IPatternDetailRepository
	{
		void Add(PatternDetailModel patternDetail);
		void Delete(int id);
		void Edit(PatternDetailModel patternDetail);
		IEnumerable<PatternDetailModel> GetAll();
		IEnumerable<PatternModel> GetAllPatterns();
		IEnumerable<RawMaterialModel> GetAllRawMaterials();
		PatternDetailModel? GetById(int id);
	}
}
