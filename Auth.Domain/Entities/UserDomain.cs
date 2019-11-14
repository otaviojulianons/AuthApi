using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Auth.Common.Domain;
using Auth.Common.Response;
using Auth.Common.Validation;

namespace Auth.Domain.Entities
{
    public class UserDomain : ISelfValidation
    {
        private Regex _permissionRegex = new Regex("^[a-zA-Z0-9]*$"); 
        private HashSet<string> _permissions;

        public UserDomain(string email, string password)
            : this(Guid.NewGuid(), email, password){}

        public UserDomain(Guid id, string email, string password, string role = UserRoles.User)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Role { get; private set; }

        public bool Admin => Role == UserRoles.Admin;

        public IReadOnlyCollection<string> Permissions => _permissions ?? new HashSet<string>();

        public void AddPermission(string permission)
        {
            if (_permissions == null)
                _permissions = new HashSet<string>();

            if(_permissionRegex.IsMatch(permission));
            _permissions.Add(permission);
        }

        public bool IsValid(BaseResponse response) => new UserValidation().IsValid(this, response);
    }
}
