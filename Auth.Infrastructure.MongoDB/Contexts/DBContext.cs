using Auth.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;

namespace Auth.Infrastructure.MongoDB.Contexts
{
    public class DBContext
    {

        public DBContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDBConnectionString");
            var mongoClient = new MongoClient(connectionString);
            Database = mongoClient.GetDatabase("Auth");

            RegisterTypes();
            RegisterUserAdmin();
        }

        private void RegisterTypes()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserDomain)))
            {

                BsonClassMap.RegisterClassMap<UserDomain>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetIdGenerator(GuidGenerator.Instance);
                    cm.MapField("_permissions").SetElementName("Permissions");
                });
            }
        }

        private void RegisterUserAdmin()
        {
            var _userCollection = Database.GetCollection<UserDomain>("Users");
            var admin = _userCollection.Find(x => x.Admin).FirstOrDefault();
            if(admin == null)
            {
                var userAdmin = new UserDomain(new Guid(), "admin", "21232f297a57a5a743894a0e4a801fc3", true);
                _userCollection.InsertOne(userAdmin);
            }
        }

        public IMongoDatabase Database { get; }
    }
}
