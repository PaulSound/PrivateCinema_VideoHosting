using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrivateCinema_VideoHosting.Data
{
    public class AuthSettings
    {
        public TimeSpan Expires { get; set; }
        public string SecretKey { get; set; }
    }
}
