using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArhivaBlanketa.Models
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _users;

        public UserServices(ISheetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("User");
        }

        public List<User> Get() =>
          _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(User => User.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(User user, User userIn)
        {
            if (userIn.Id == null)
                userIn.Id = user.Id;

            _users.ReplaceOne(usr => usr.Id == user.Id, userIn);
        }

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(User => User.Id == id);
    }
}
