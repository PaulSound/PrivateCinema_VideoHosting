using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace PrivateCinema_VideoHosting.Data
{
    public static class AuthExtensions
    {
        
        public static IServiceCollection AddAuth(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            var authStting = configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>();
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authStting.SecretKey))
                });
            return serviceCollection;
        }
    }
}
