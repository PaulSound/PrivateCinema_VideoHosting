using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Models;
using PrivateCinema_VideoHosting.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IO;

namespace PrivateCinema_VideoHosting.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(ApplicationContext contextDb, UserService userService, IConfiguration configuration)
        {
            _context = contextDb;
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(RegistrationModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Login == obj.Password)
                {
                    ModelState.AddModelError("", "The Login cannot excatly match The password");
                    return View();
                }
                else
                {
                    if (null != _context._signInList.Where(x => x.Email == obj.Email).FirstOrDefault())
                    {
                        ModelState.AddModelError("", "The user with the inserted Email was already used. Use new email for signing up");
                        return View();
                    }
                    User newUser = _userService.CreateNewUser(obj);
                    _context._userList.Add(newUser);
                    _context.SaveChanges();
                    TempData["added"] = "The new user has been created successfully";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpPatch]
        [Authorize]
        public IActionResult Update()
        {
            return View();
        }
        
    }
}
