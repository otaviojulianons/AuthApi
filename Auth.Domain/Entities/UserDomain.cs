using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public class UserDomain
    {
        private HashSet<string> _permissions;

        public UserDomain(string email, string password)
            : this(Guid.NewGuid(), email, password){}

        public UserDomain(Guid id, string email, string password, bool admin = false)
        {
            Id = id;
            Email = email;
            Password = password;
            Admin = admin;
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool Admin { get; private set; }

        public IReadOnlyCollection<string> Permissions => _permissions ?? new HashSet<string>();

        public void AddPermission(string permission)
        {
            if (_permissions == null)
                _permissions = new HashSet<string>();
            _permissions.Add(permission);
        } 

    }
}
