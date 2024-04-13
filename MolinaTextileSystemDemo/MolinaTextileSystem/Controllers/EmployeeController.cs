using Microsoft.AspNetCore.Mvc;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Employees;

namespace MolinaTextileSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View(_employeeRepository.GetAll());
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
        public ActionResult Create(EmployeeModel employee)
        {
            try
            {
                _employeeRepository.Add(employee);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(employee);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var publisher = _employeeRepository.GetById(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeModel employee)
        {
            try
            {
                _employeeRepository.Edit(employee);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(employee);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EmployeeModel employee)
        {
            try
            {
                _employeeRepository.Delete(employee.EmployeeId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(employee);
            }
        }
    }
}
