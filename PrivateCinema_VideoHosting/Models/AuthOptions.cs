using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MySuperBlog.Server.Models
{
    public class AuthOptions // класса для настройки валидации ключа авторизации
    {
        public const string ISSUER = "MyAuthServer"; // издатель
        public const string AUDIENCE = "MyAuthCLient"; // получатель токена
        const string KEY = "mysupersecret_secretkey!123MyAuthServerCertificationPrimaryStatus"; //ключ токена
        public const int LIFETIME = 10; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
