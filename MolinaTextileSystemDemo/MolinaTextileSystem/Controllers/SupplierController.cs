using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Suppliers;

namespace MolinaTextileSystem.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public ActionResult Index()
        {
            return View(_supplierRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierModel supplier)
        {
            try
            {
                _supplierRepository.Add(supplier);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(supplier);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var publisher = _supplierRepository.GetById(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierModel supplier)
        {
            try
            {
                _supplierRepository.Edit(supplier);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(supplier);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var supplier = _supplierRepository.GetById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SupplierModel supplier)
        {
            try
            {
                _supplierRepository.Delete(supplier.SupplierId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(supplier);
            }
        }
    }
}
