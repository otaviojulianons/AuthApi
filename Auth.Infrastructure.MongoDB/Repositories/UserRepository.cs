using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Infrastructure.MongoDB.Contexts;
using MongoDB.Driver;

namespace Auth.Infrastructure.MongoDB.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDomain> _collection;

        public UserRepository(DBContext context)
        {
            _collection = context.Database.GetCollection<UserDomain>("Users");
        }

        public UserDomain GetUserByEmail(string email)
        {
            return _collection.Find(x => x.Email == email).FirstOrDefault();
        }
    }
}
