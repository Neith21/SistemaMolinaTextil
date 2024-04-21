using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Products
{
    public interface IProductRepository
    {
        void Add(ProductModel product);
        void Delete(int id);
        void Edit(ProductModel product);
        IEnumerable<ProductModel> GetAll();
        IEnumerable<PatternModel> GetAllPattern();
        IEnumerable<StateModel> GetAllState();
        ProductModel? GetById(int id);
    }
}