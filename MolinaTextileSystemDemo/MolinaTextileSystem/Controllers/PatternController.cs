using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Pattern;

namespace MolinaTextileSystem.Controllers
{
    public class PatternController : Controller
    {
        private readonly IPatternRepository _patternRepository;


        public PatternController(IPatternRepository patternRepository)
        {
            _patternRepository = patternRepository;

        }

        public ActionResult Index()
        {
            return View(_patternRepository.GetAll());
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatternModel pattern)
        {
            try
            {
                _patternRepository.Add(pattern);

                TempData["createpattern"] = "Datos guardados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(pattern);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pattern = _patternRepository.GetById(id);

            if (pattern == null)
            {
                return NotFound();
            }

            return View(pattern);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatternModel pattern)
        {
            try
            {
                _patternRepository.Edit(pattern);

                TempData["editpattern"] = "Datos editados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(pattern);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var pattern = _patternRepository.GetById(id);

            if (pattern == null)
            {
                return NotFound();
            }

            return View(pattern);
        }

        // POST: SuppliersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PatternModel pattern)
        {
            try
            {
                _patternRepository.Delete(pattern.PatternId);

                TempData["deletecustomers"] = "Dato eliminados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(pattern);
            }
        }
    }
}
