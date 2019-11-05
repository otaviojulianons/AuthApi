namespace Auth.Application.Messages
{
    public class UserAuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
