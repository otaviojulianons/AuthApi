using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Infrastructure.MongoDB.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Auth.Infrastructure.MongoDB.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDomain> _collection;

        public UserRepository(DBContext context)
        {
            _collection = context.Database.GetCollection<UserDomain>("Users");
        }

        public IEnumerable<UserDomain> GetAll()
        {
            return _collection.AsQueryable();
        }

        public UserDomain GetUserById(Guid id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public UserDomain GetUserByEmail(string email)
        {
            return _collection.Find(x => x.Email == email).FirstOrDefault();
        }

        public bool Insert(UserDomain user)
        {
            _collection.InsertOne(user);
            return true;
        }

        public bool Update(UserDomain user)
        {
            var result =_collection.ReplaceOne(x => x.Id == user.Id, user);
            return result.ModifiedCount > 0;
        }

        public bool Delete(Guid id)
        {
            var result = _collection.DeleteOne(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
