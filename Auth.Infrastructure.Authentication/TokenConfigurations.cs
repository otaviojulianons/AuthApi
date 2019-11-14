namespace Auth.Infrastructure.Jwt
{
    public class TokenConfigurations
    {
        public string Secret { get; set; }
        public int Seconds { get; set; }
    }
}
