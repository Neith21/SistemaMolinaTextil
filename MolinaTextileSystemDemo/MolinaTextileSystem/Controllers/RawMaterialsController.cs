using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.RawMeterials;

namespace MolinaTextileSystem.Controllers
{
    public class RawMaterialsController : Controller
    {
        private readonly IRawMaterialsRepository _rawMaterialsRepository;

        private SelectList _categoryList;
        private SelectList _supplierList;

        public RawMaterialsController(IRawMaterialsRepository rawMaterialsRepository)
        {
            _rawMaterialsRepository = rawMaterialsRepository;
            _categoryList = new SelectList(
            _rawMaterialsRepository.GetAllCategories(),
                nameof(CategoryModel.CategoryId),
                nameof(CategoryModel.CategoryName)
            );

            _supplierList = new SelectList(
                _rawMaterialsRepository.GetAllSuppliers(),
                nameof(SupplierModel.SupplierId),
                nameof(SupplierModel.SupplierName)
            );
        }
        // GET: RawMaterialsController
        public ActionResult Index()
        {
            return View(_rawMaterialsRepository.GetAll());
        }

        // GET: RawMaterialsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RawMaterialsController/Create
        public ActionResult Create()
        {
            ViewBag.Category = _categoryList;
            ViewBag.Supplier = _supplierList;

            return View();
        }

        // POST: RawMaterialsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RawMaterialsModel rawMaterials)
        {
            try
            {
                _rawMaterialsRepository.Add(rawMaterials);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Category = _categoryList;
                ViewBag.Supplier = _supplierList;

                return View(rawMaterials);
            }
        }

        // GET: RawMaterialsController/Edit/5
        public ActionResult Edit(int id)
        {
            var rawMaterials = _rawMaterialsRepository.GetById(id);

            if (rawMaterials == null)
            {
                return NotFound();
            }

            _categoryList = new SelectList(
                _rawMaterialsRepository.GetAllCategories(),
                nameof(CategoryModel.CategoryId),
                nameof(CategoryModel.CategoryName),
                rawMaterials?.Category?.CategoryId
            );

            _supplierList = new SelectList(
                _rawMaterialsRepository.GetAllSuppliers(),
                nameof(SupplierModel.SupplierId),
                nameof(SupplierModel.SupplierName),
                rawMaterials?.Supplier?.SupplierId
            );

            ViewBag.Category = _categoryList;
            ViewBag.Supplier = _supplierList;

            return View(rawMaterials);
        }

        // POST: RawMaterialsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RawMaterialsModel rawMaterials)
        {
            try
            {
                _rawMaterialsRepository.Edit(rawMaterials);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Category = _categoryList;
                ViewBag.Supplier = _supplierList;

                return View(rawMaterials);
            }
        }

        // GET: RawMaterialsController/Delete/5
        public ActionResult Delete(int id)
        {
            var rawMaterials = _rawMaterialsRepository.GetById(id);

            if (rawMaterials == null)
            {
                return NotFound();
            }

            return View(rawMaterials);
        }

        // POST: RawMaterialsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RawMaterialsModel rawMaterials)
        {
            try
            {
                _rawMaterialsRepository.Delete(rawMaterials.RawMaterialId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(rawMaterials);
            }
        }
    }
}
