namespace Azure_API_Test.Request
{
    public class Request
    {
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class RefreshTokenRequest
        {
            public string RefreshToken { get; set; }
        }
    }
}
