using Microsoft.AspNetCore.Mvc;
using MolinaTextilSystem.Models;
using MolinaTextilSystem.Repositories.InventoryRawMaterials;

namespace MolinaTextilSystem.Controllers
{
    public class InventoryRawMaterialsController : Controller
    {
        private readonly IInventoryRawMaterialsRepository _inventoryRawMaterialsRepository;

        public InventoryRawMaterialsController(IInventoryRawMaterialsRepository inventoryRawMaterialsRepository)
        {
            _inventoryRawMaterialsRepository = inventoryRawMaterialsRepository;
        }

        // GET: InventoryRawMaterialsController
        public ActionResult Index()
        {
            return View(_inventoryRawMaterialsRepository.GetAll());
        }

        // GET: InventoryRawMaterialsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UniversityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryRawMaterialsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryRawMaterialsModel InventoryRawMaterials)
        {
            try
            {
                _inventoryRawMaterialsRepository.Add(InventoryRawMaterials);

                TempData["message"] = "Datos guardados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryRawMaterialsController/Edit/5
        public ActionResult Edit(int id)
        {
            var InventoryRawMaterials = _inventoryRawMaterialsRepository.GetById(id);

            if (InventoryRawMaterials == null)
            {
                return NotFound();
            }

            return View(InventoryRawMaterials);
        }

        // POST: InventoryRawMaterialsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryRawMaterialsModel InventoryRawMaterials)
        {
            try
            {
                _inventoryRawMaterialsRepository.Edit(InventoryRawMaterials);

                TempData["message"] = "Datos actualizados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(InventoryRawMaterials);
            }
        }

        // GET:InventoryRawMaterialsController/Delete/5
        public ActionResult Delete(int id)
        {
            var InventoryRawMaterials = _inventoryRawMaterialsRepository.GetById(id);

            if (InventoryRawMaterials == null)
            {
                return NotFound();
            }

            return View(InventoryRawMaterials);
        }

        // POST: InventoryRawMaterialsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(InventoryRawMaterialsModel InventoryRawMaterials)
        {
            try
            {
                _inventoryRawMaterialsRepository.Delete(InventoryRawMaterials.RawMaterialID);

                TempData["message"] = "Datos eliminados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(InventoryRawMaterials);
            }
        }
    }
}
