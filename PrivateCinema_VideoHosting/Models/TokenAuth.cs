namespace MySuperBlog.Server.Models
{
    public class TokenAuth
    {
        public int Minutes { get; private set; }
        public string AccessToken { get; private set; }
        public string Username { get; private set; }
        public int UserId { get; private set; }
        public TokenAuth(
            int minutes,
            string accessToken,
            string userName,
            int userId)
        {
            Minutes = minutes;
            AccessToken = accessToken;
            Username = userName;
            UserId = userId;
        }
    }
}
