using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MolinaTextileSystem.Controllers
{
    public class LoginCredentialController : Controller
    {
        // GET: LoginCredentialController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginCredentialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginCredentialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginCredentialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginCredentialController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginCredentialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginCredentialController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginCredentialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
