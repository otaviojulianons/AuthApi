using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public class UserDomain
    {
        public Guid Id { get; set; }

        public bool Admin { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserGroup Group { get; set; }

        public IEnumerable<PermissionDomain> Permissions { get; set; }

    }
}
