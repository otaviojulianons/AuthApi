using Auth.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<UserDomain> GetAll();
        public UserDomain GetUserById(Guid id);
        public UserDomain GetUserByEmail(string email);
        bool Insert(UserDomain user);
        bool Update(UserDomain user);
        bool Delete(Guid id);
    }
}
