using Microsoft.AspNetCore.Mvc;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Estado;

namespace MolinaTextileSystem.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public ActionResult Index()
        {
            return View(_stateRepository.GetAll());
        }

        // GET: StateController/Details/5
        public ActionResult Details(int id)
        {
            var state = _stateRepository.GetById(id);

            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: StateController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateModel state)
        {
            try
            {
                _stateRepository.Add(state);

                TempData["createstate"] = "Datos guardados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(state);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var state = _stateRepository.GetById(id);

            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StateModel state)
        {
            try
            {
                _stateRepository.Edit(state);

                TempData["editstate"] = "Datos editados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(state);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var state = _stateRepository.GetById(id);

            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: StateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(StateModel state)
        {
            try
            {
                _stateRepository.Delete(state.StateId);

                TempData["deletestate"] = "Dato eliminado exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(state);
            }
        }

    }
}
