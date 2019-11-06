using System.Collections.Generic;

namespace Auth.Application.Messages
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
