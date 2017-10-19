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

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Quiz");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(string name, string password, string confirmPassword)
        {
            var user = _userService.CreateUser();
            user.Name = name;
            user.Password = password;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Username and Password are required");
                return View();
            }
            if (user.Password.Length < 7)
            {
                ModelState.AddModelError(string.Empty, "Password must contain at least 7 symbols");
                return View();
            }
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password confirmation doesn't match password");
                return View();
            }
            var validationResult = await _userService.RegisterAsync(name, password);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View();
            }

            FormsAuthentication.SetAuthCookie(name, false);
            return RedirectToAction("Index", "Quiz");
        }
    }
}