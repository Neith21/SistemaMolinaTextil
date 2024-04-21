using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.CustomersOrdersDetails;

namespace MolinaTextileSystem.Controllers
{
    public class CustomerOrderDetailController : Controller
    {
        public readonly ICustomersOrdersDetailsRepository _customersOrdersDetailsRepository;
        private SelectList _productList;

        public CustomerOrderDetailController(ICustomersOrdersDetailsRepository customersOrdersDetailsRepository)
        {
            _customersOrdersDetailsRepository = customersOrdersDetailsRepository;

            _productList = new SelectList(
                _customersOrdersDetailsRepository.GetAllProduct(),
                nameof(ProductModel.ProductId),
                nameof(ProductModel.ProductName)
            );
        }

        // GET: CustomerOrderDetailController
        public ActionResult Index()
        {
            return View(_customersOrdersDetailsRepository.GetAll());
        }

        // GET: CustomerOrderDetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerOrderDetailController/Create
        public ActionResult Create()
        {
            ViewBag.Product = _productList;

            var customerOrderIds = _customersOrdersDetailsRepository.GetAllCustomerOrder().ToList();
            ViewBag.CustomerOrder = customerOrderIds;

            return View();
        }

        // POST: CustomerOrderDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerOrderDetailModel customerOrderDetailModel)
        {
            try
            {
                _customersOrdersDetailsRepository.Add(customerOrderDetailModel);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Product = _productList;

                return View(customerOrderDetailModel);
            }
        }

        // GET: CustomerOrderDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            var customerOrderDetail = _customersOrdersDetailsRepository.GetById(id);

            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            _productList = new SelectList(
                _customersOrdersDetailsRepository.GetAllProduct(),
                nameof(ProductModel.ProductId),
                nameof(ProductModel.ProductName),
                customerOrderDetail?.Producto?.ProductId
            );

            ViewBag.Product = _productList;

            return View(customerOrderDetail);
        }

        // POST: CustomerOrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerOrderDetailModel customerOrderDetail)
        {
            try
            {
                _customersOrdersDetailsRepository.Edit(customerOrderDetail);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Product = _productList;

                return View();
            }
        }

        // GET: CustomerOrderDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            var customerOrderDetail = _customersOrdersDetailsRepository.GetById(id);

            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            return View(customerOrderDetail);
        }

        // POST: CustomerOrderDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerOrderDetailModel customerOrderDetailModel)
        {
            try
            {
                _customersOrdersDetailsRepository.Delete(customerOrderDetailModel.CustomerOrderDetailId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(customerOrderDetailModel);
            }
        }
    }
}
