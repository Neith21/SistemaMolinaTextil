using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.CustomerOrders;

namespace MolinaTextileSystem.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;

        private SelectList _customersList;
        private SelectList _employeesList;
        private SelectList _statesList;

        public CustomerOrderController(ICustomerOrderRepository customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _customersList = new SelectList(
                _customerOrderRepository.GetAllCustomers(),
                nameof(CustomerModel.CustomerId),
                nameof(CustomerModel.CustomerName)
            );
            _employeesList = new SelectList(
                _customerOrderRepository.GetAllEmployees(),
                nameof(EmployeeModel.EmployeeId),
                nameof(EmployeeModel.EmployeeName)
            );
            _statesList = new SelectList(
                _customerOrderRepository.GetAllStates(),
                nameof(StateModel.StateId),
                nameof(StateModel.StateName)
            );
        }

        // GET: CustomerOrderController
        public ActionResult Index()
        {
            return View(_customerOrderRepository.GetAll());
        }

        // GET: CustomerOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerOrderController/Create
        public ActionResult Create()
        {
            ViewBag.Customers = _customersList;
            ViewBag.Employees = _employeesList;
            ViewBag.States = _statesList;

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

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Customers = _customersList;
                ViewBag.Employees = _employeesList;
                ViewBag.States = _statesList;

                return View(customerOrder);
            }
        }

        // GET: CustomerOrderController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customerOrder = _customerOrderRepository.GetById(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            _customersList = new SelectList(
                _customerOrderRepository.GetAllCustomers(),
                nameof(CustomerModel.CustomerId),
                nameof(CustomerModel.CustomerName),
                customerOrder?.Customer?.CustomerId
            );
            _employeesList = new SelectList(
                _customerOrderRepository.GetAllEmployees(),
                nameof(EmployeeModel.EmployeeId),
                nameof(EmployeeModel.EmployeeName),
                customerOrder?.Employee?.EmployeeId
            );
            _statesList = new SelectList(
                _customerOrderRepository.GetAllStates(),
                nameof(StateModel.StateId),
                nameof(StateModel.StateName),
                customerOrder?.State?.StateId
            );

            ViewBag.Customers = _customersList;
            ViewBag.Employees = _employeesList;
            ViewBag.States = _statesList;

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

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Customers = _customersList;
                ViewBag.Employees = _employeesList;
                ViewBag.States = _statesList;

                return View(customerOrder);
            }
        }

        // GET: CustomerOrderController/Delete/5
        [HttpGet]
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
        public ActionResult Delete(CustomerOrderModel customerOrder)
        {
            try
            {
                _customerOrderRepository.Delete(customerOrder.CustomerOrderId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(customerOrder);
            }
        }
    }
}
