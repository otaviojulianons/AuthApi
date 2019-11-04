using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PermissionDomain> Permissions { get; set; }
    }
}
