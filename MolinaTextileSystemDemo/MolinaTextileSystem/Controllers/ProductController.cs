using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.Products;

namespace MolinaTextileSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        private SelectList _patternList;
        private SelectList _stateList;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _patternList = new SelectList(
                _productRepository.GetAllPattern(),
                nameof(PatternModel.PatternId),
                nameof(PatternModel.PatternName)
            );

            _stateList = new SelectList(
                _productRepository.GetAllState(),
                nameof(StateModel.StateId),
                nameof(StateModel.StateName)
            );
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Pattern = _patternList;
            ViewBag.State = _stateList;

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                _productRepository.Add(product);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Pattern = _patternList;
                ViewBag.State = _stateList;

                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _patternList = new SelectList(
                _productRepository.GetAllPattern(),
                nameof(PatternModel.PatternId),
                nameof(PatternModel.PatternName),
                product?.Pattern?.PatternId
            );

            _stateList = new SelectList(
                _productRepository.GetAllState(),
                nameof(StateModel.StateId),
                nameof(StateModel.StateName),
                product?.States?.StateId
            );

            ViewBag.Pattern = _patternList;
            ViewBag.State = _stateList;

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel product)
        {
            try
            {
                _productRepository.Edit(product);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                ViewBag.Pattern = _patternList;
                ViewBag.State = _stateList;

                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductModel product)
        {
            try
            {
                _productRepository.Delete(product.ProductId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(product);
            }
        }
    }
}
