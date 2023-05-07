

namespace Application.Model
{
    public class LoginApi
    {
        public string? UserName
        {
            get;
            set;
        }
        public string? Password
        {
            get;
            set;
        }
    }

    public class JWTTokenResponse
    {
        public string? Token
        {
            get;
            set;
        }
    }
}
