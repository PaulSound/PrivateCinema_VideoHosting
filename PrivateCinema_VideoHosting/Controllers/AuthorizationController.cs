using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Models;
using PrivateCinema_VideoHosting.Services;


namespace PrivateCinema_VideoHosting.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public AuthorizationController(ApplicationContext contextDb, UserService userService, IConfiguration configuration)
        {
            _context = contextDb;
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel obj) 
        {
            if(ModelState.IsValid)
            {
                var user=_userService.GetUserByLogin(obj.Login);
                if(user!=null) 
                {
                    var result= new PasswordHasher<SignIn>().VerifyHashedPassword(user, user.Password, obj.Password);
                    if (result==PasswordVerificationResult.Success)
                    {
                        await _userService.CreateAuthentication(user, HttpContext);
                        TempData["info"] = "You successfully have logged in";
                        return RedirectToAction("Index","Home",ToUserModel(user));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This user doesn't exist or you've inserted a wrong login or password");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        private UserModel ToUserModel(SignIn user)
        {
            return new UserModel() { Id=user.Id, Name=user.Login,LibraryNumb=_userService.GetLibraryNumber(user.Id) };
        }
    }
}
