using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    // o Authorize verifica se o usuário está autenticado...pode ser usado em cada método ou na classe toda
    // [Authorize(Roles = "Admin")]   --> verifica se o usuário está autenticado e se pertence ao perfil Admin
    // [Authorize]
    public class ContatoController : Controller
    {
        /*
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        */

        // [AllowAnonymous] --> esse AllowAnonymoys permite o acesso a usuário não autenticado ao método Index...pode ser usado em cada método ou na classe toda
        public IActionResult Index()
        {
            return View();
        }
    }
}
