using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Employees;

namespace Molina_Textil_Inventory_System.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
          
        }

        public ActionResult Index()
        {
            return View(_customerRepository.GetAll());
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
        public ActionResult Create(CustomerModel customer)
        {
            try
            {
                _customerRepository.Add(customer);

                TempData["createcustomers"] = "Datos guardados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(customer);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel customer)
        {
            try
            {
                _customerRepository.Edit(customer);

                TempData["editcustomers"] = "Datos editados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(customer);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: SuppliersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerModel customer)
        {
            try
            {
                _customerRepository.Delete(customer.CustomerId);

                TempData["deletecustomers"] = "Dato eliminados exitosamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(customer);
            }
        }
    }
}
