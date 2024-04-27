using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Category
{
    public interface ICategoryRepository
    {
        void Add(CategoryModel categoryModel);
        void Delete(int id);
        void Edit(CategoryModel categoryModel);
        IEnumerable<CategoryModel> GetAll();
        CategoryModel? GetById(int id);
    }
}
