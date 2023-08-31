using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login (string ReturnUrl)
        {
            return View(new LoginViewModel()
                {
                    ReturnUrl = ReturnUrl
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            // quando é async tem que usar o await
            var user = await _userManager.FindByNameAsync(login.UserName);  // pesquisa se o usuário existe

            if (user != null)   // ou seja, o usuário foi encontrado
            {
                // informo o password ao usuário
                // o "false" são cookies de entrada e o outro false é pra bloquear a conta se o login falhar
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(login.ReturnUrl);
                }
            }
            ModelState.AddModelError("Erro", "Falha ao realizar o login!");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // esse validate evita o ataque de falsificação de requisições entre sites (CSRF)...valida o token gerado na view
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }
            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();   // remove todos os valores dos objetos na session
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
