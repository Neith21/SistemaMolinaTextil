using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.RawMeterials
{
    public interface IRawMaterialsRepository
    {
        void Add(RawMaterialsModel materials);
        void Delete(int id);
        void Edit(RawMaterialsModel materials);
        IEnumerable<RawMaterialsModel> GetAll();
        IEnumerable<CategoryModel> GetAllCategories();
        IEnumerable<SupplierModel> GetAllSuppliers();
        RawMaterialsModel? GetById(int id);
    }
}