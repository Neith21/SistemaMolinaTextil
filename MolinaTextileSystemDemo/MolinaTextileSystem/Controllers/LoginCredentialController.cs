using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MolinaTextileSystem.Models;
using MolinaTextileSystem.Repositories.LoginCredentials;

namespace MolinaTextileSystem.Controllers
{
    public class LoginCredentialController : Controller
    {
        private readonly ILoginCredentialRepository _loginCredentialRepository;

        private SelectList _rolesList;
        private SelectList _employeesList;

        public LoginCredentialController(ILoginCredentialRepository loginCredentialRepository)
        {
            _loginCredentialRepository = loginCredentialRepository;
			_rolesList = new SelectList(
				_loginCredentialRepository.GetAllRoles(),
				nameof(RolModel.rolId),
				nameof(RolModel.rolName)
			);
			_employeesList = new SelectList(
                _loginCredentialRepository.GetAllEmployees(),
                nameof(EmployeeModel.EmployeeId),
                nameof(EmployeeModel.EmployeeName)
            );
        }

        // GET: LoginCredentialController
        public ActionResult Index()
        {
            return View(_loginCredentialRepository.GetAll());
        }

        // GET: LoginCredentialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginCredentialModel credentialModel)
        {
            var credential = _loginCredentialRepository.GetCredentials(credentialModel.Username, credentialModel.Password);

            if (credential != null)
            {
                TempData["RolUsername"] = credential.Username;
                // Redirigir al usuario a la vista del dashboard general
                return RedirectToAction("Index", "CustomerOrder");
            }
            else
            {
                TempData["messageLogin"] = "Usuario o Contraseña Incorrectos, Vuelva a Intentarlo";
            }

            return View(credentialModel);
        }

        // GET: LoginCredentialController/Create
        public ActionResult Create()
        {

            ViewBag.Roles = _rolesList;
            ViewBag.Employees = _employeesList;

            return View();
        }

        // POST: LoginCredentialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginCredentialModel loginCredential)
        {
            try
            {
                _loginCredentialRepository.Add(loginCredential);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

				ViewBag.Roles = _rolesList;
				ViewBag.Employees = _employeesList;

                return View(loginCredential);
            }
        }

        // GET: LoginCredentialController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var loginCredential = _loginCredentialRepository.GetById(id);

            if (loginCredential == null)
            {
                return NotFound();
            }

			_rolesList = new SelectList(
				_loginCredentialRepository.GetAllRoles(),
				nameof(RolModel.rolId),
				nameof(RolModel.rolName),
                loginCredential?.Rol?.rolId
			);
			_employeesList = new SelectList(
                _loginCredentialRepository.GetAllEmployees(),
                nameof(EmployeeModel.EmployeeId),
                nameof(EmployeeModel.EmployeeName),
                loginCredential?.Employee?.EmployeeId
            );

			ViewBag.Roles = _rolesList;
			ViewBag.Employees = _employeesList;

            return View(loginCredential);
        }

        // POST: LoginCredentialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginCredentialModel loginCredential)
        {
            try
            {
                _loginCredentialRepository.Edit(loginCredential);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

				ViewBag.Roles = _rolesList;
				ViewBag.Employees = _employeesList;

                return View(loginCredential);
            }
        }

        // GET: LoginCredentialController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var loginCredential = _loginCredentialRepository.GetById(id);

            if (loginCredential == null)
            {
                return NotFound();
            }

            return View(loginCredential);
        }

        // POST: LoginCredentialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(LoginCredentialModel loginCredential)
        {
            try
            {
                _loginCredentialRepository.Delete(loginCredential.LoginCredentialId);

                TempData["message"] = "Dato eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(loginCredential);
            }
        }
    }
}
