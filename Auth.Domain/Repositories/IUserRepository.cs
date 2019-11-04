using Auth.Domain.Entities;

namespace Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        public UserDomain GetUserByEmail(string email);
    }
}
