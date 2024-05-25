using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.CustomerOrders;
using MolinaTextileSystem.Repositories.CustomersOrdersDetails;

namespace MolinaTextileSystem.Controllers
{
    [Authorize]
    public class CustomerOrderController : Controller
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly ICustomersOrdersDetailsRepository _customersOrdersDetailsRepository;

        private SelectList _customersList;
        private SelectList _employeesList;
        private SelectList _statesList;

        public CustomerOrderController(ICustomerOrderRepository customerOrderRepository, ICustomersOrdersDetailsRepository customersOrdersDetailsRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _customersOrdersDetailsRepository = customersOrdersDetailsRepository;
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

        public ActionResult GetAllCustomerOrderDetails(int id)
        {
            ViewBag.CustomerOrderId = id;
            return View(_customersOrdersDetailsRepository.GetSpecificById(id));
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
                int id = _customerOrderRepository.Add(customerOrder);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction("GetAllCustomerOrderDetails", new { id });
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
