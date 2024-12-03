using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PrivateCinema_VideoHosting.Services
{
    public class UserService
    {
        private ApplicationContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationContext context, IConfiguration configuration,DatabaseService databaseService)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public async Task CreateAuthentication(SignIn user, HttpContext context)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Login)
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                props);
        }
        public User? GetUserByClaim(ClaimsPrincipal principal)
        {

            var userClaim = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).SingleOrDefault();
            int userId;

            if (!int.TryParse(userClaim, out userId))
            {
                return null;
            }

            return GetUserById(userId);
        }
    
        public SignIn? GetUserByLogin(string login) // Получить пользователя по его логина(email) логин будет извлекаться из http апроса
        {
            var data = _dbContext._signInList.FirstOrDefault(x => x.Login == login);
            return data;
        }
        public User GetUserById(int userId)
        {
            var currentUser = _dbContext._userList.Where(x => x.SignInId == userId).First();
            return currentUser;
        }
        public User CreateNewUser(RegistrationModel obj)
        {
            SignIn signInObj = new SignIn() { Login = obj.Login, Email = obj.Email };
            string hashedPassword = new PasswordHasher<SignIn>().HashPassword(signInObj, obj.Password);
            signInObj.Password = hashedPassword;
            string collectionFolder = VideoCollection.GetNewCollectionFolder();
            User newUser = new User() { Name = obj.Name, SecondName = obj.Surname, SignIn = signInObj, LibraryNumb = Path.GetFileName(collectionFolder) };
            return newUser;
        }
        public (int userId, string path) GetUploadPath(int currentUserId, string fName)
        {
            var uploadPath = $"{VideoCollection.GetCollectionFolder()}"; // имя диррективы
            string libNumb = GetLibraryNumber(currentUserId); // номер библиотеки
            var fileName = fName; // имя файла с расширением
            var filePath = Path.Combine(uploadPath, libNumb); // имя диррективы с номером библиотеки
            filePath = Path.Combine(filePath, fileName);
            
            return (currentUserId, filePath);
        }
        public string GetLibraryNumber(int userId)
        {
            string libraryNumber = _dbContext._userList.Where(x => x.SignInId == userId).Select(x => x.LibraryNumb).First();
            return libraryNumber;
        }
        public  bool VerifyHashedPassword(SignIn user,string dbPassword, string hashedPassword) // меотд который проверит зашифрованный код из БД с тем что вводится пользователем
        {
            var  result= new PasswordHasher<SignIn>().VerifyHashedPassword(user,dbPassword,hashedPassword);
            return result==PasswordVerificationResult.Success;
        }
    }
}
