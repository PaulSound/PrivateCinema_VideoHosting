using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Hubs;
using PrivateCinema_VideoHosting.Models;
using PrivateCinema_VideoHosting.Services;
using System.Data;
using System.Diagnostics;

namespace PrivateCinema_VideoHosting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        private readonly ApplicationContext _context;
        private readonly UserService _userService;
        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContextDB, UserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _context = applicationContextDB;
            _configuration = configuration;
            _userService= userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("UploadFilePost")]   
        [RequestSizeLimit(512*1024*1024)]
        public async Task<IActionResult> UploadFilePost(IFormFile file)
        {
            if (file == null)
            {
                TempData["error"] = "You have not chosen a file to upload! Try again!";
                return View("Index");
            }
            var currentUser = _userService.GetUserByLogin(HttpContext.User.Identity.Name);
            var uploadPath_Name = _userService.GetUploadPath(currentUser.Id,file.FileName);

            if (Path.Exists(uploadPath_Name.path))
            {
                TempData["warning"] = $"The file with {Path.GetFileNameWithoutExtension(uploadPath_Name.path)} already exists! Rename uploading file and try again!";
                return View("Index");
            }
            Video newVideo = new Video() { FileName = file.FileName, FilePath = uploadPath_Name.path, UploadTime = DateTime.Now,userId=uploadPath_Name.userId}; // User=currentUser FilePath

            await _context._videoList.AddAsync(newVideo);
            await _context.SaveChangesAsync();
        

            var stream = new FileStream(uploadPath_Name.path, FileMode.Create, FileAccess.ReadWrite);
            await file.CopyToAsync(stream);
            stream.Dispose();

            return RedirectToAction("Index");
        }
        public IActionResult LoadVideo()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
