using System;
using System.Collections.Generic;

namespace Auth.Application.Messages
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public bool Admin { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
