using MolinaTextilSystem.Models;

namespace MolinaTextilSystem.Repositories.InventoryRawMaterials
{
    public interface IInventoryRawMaterialsRepository
    {
        void Add(InventoryRawMaterialsModel InventoryRawMaterial);
        void Delete(int id);
        void Edit(InventoryRawMaterialsModel InventoryRawMaterial);
        IEnumerable<InventoryRawMaterialsModel> GetAll();
        InventoryRawMaterialsModel? GetById(int id);
    }
}
