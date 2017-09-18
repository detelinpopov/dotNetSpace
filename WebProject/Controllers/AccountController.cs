using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Interfaces.Core.Services;

namespace WebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]        
        [AllowAnonymous]
        public async Task<ActionResult> Login(string name, string password)
        {
            var isValidUser = await _userService.IsValidUser(name, password);
            if (isValidUser)
            {
                FormsAuthentication.SetAuthCookie(name, false);
                return RedirectToAction("Index", "Quiz");
            }

            ModelState.AddModelError("", "Login details are wrong.");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(string name, string password)
        {
            var user = _userService.CreateUser();
            user.Name = name;
            user.Password = password;
            var validationResult = await _userService.RegisterAsync(name, password);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }

            return RedirectToAction("Index", "Quiz");
        }
    }
}