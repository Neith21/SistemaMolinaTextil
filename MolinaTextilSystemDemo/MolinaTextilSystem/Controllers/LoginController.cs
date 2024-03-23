using Microsoft.AspNetCore.Mvc;
using MolinaTextilSystem.Models;
using MolinaTextilSystem.Repositories.InventoryRawMaterials;
using MolinaTextilSystem.Repositories.Login;

namespace MolinaTextilSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _IloginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _IloginRepository = loginRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CredentialsModel credentialModel)
        {
            if (ModelState.IsValid)
            {
                var credential = _IloginRepository.GetByUsernameEmployee(credentialModel.EmployeeUsername, credentialModel.EmployeePassword);

                if (credential != null)
                {
                    // Redirigir al usuario a la vista del dashboard general
                    return RedirectToAction("Index", "CustomerOrder", new { EmployeeRol = credential.EmployeeRolID });
                }
                else
                {
                    TempData["messageLogin"] = "Usuario o Contraseña Incorrectos, Vuelva a Intentarlo";
                }
            }

            return View(credentialModel);
        }
    }
}
