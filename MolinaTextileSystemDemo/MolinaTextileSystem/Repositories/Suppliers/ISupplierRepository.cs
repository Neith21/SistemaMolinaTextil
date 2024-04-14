using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.Suppliers
{
    public interface ISupplierRepository
    {
        void Add(SupplierModel supplier);
        void Delete(int id);
        void Edit(SupplierModel supplier);
        IEnumerable<SupplierModel> GetAll();
        SupplierModel? GetById(int id);
    }
}