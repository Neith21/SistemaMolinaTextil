using Microsoft.AspNetCore.Mvc;
using MolinaTextilSystem.Models;
using MolinaTextilSystem.Repositories.CustomerOrder;
using Dapper;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
namespace MolinaTextilSystem.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public CustomerOrderController(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }

        // GET: CustomerOrderController
        public ActionResult Index(EmployeeRol employeeRol) 
        {
            ViewData["EmployeeRol"] = employeeRol;

            return View(_customerOrderRepository.GetAll());
        }

        // GET: CustomerOrderController/Details/5
        public ActionResult Details(int id)
        {
            var customerOrder = _customerOrderRepository.GetById(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // GET: CustomerOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerOrderModel customerOrder)
        {
            try
            {
                _customerOrderRepository.Add(customerOrder);

                TempData["message"] = "Datos guardados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var customerOrder = _customerOrderRepository.GetById(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // POST: CustomerOrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerOrderModel customerOrder)
        {
            try
            {
                _customerOrderRepository.Edit(customerOrder);

                TempData["message"] = "Datos actualizados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(customerOrder);
            }
        }

        // GET: CustomerOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            var customerOrder = _customerOrderRepository.GetById(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // POST: CustomerOrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerOrderModel customerOrder)
        {
            try
            {
                _customerOrderRepository.Delete(id);

                TempData["message"] = "Datos eliminados con éxito.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Exception exception = ex;

                return View(customerOrder);
            }
        }   


    }
}
