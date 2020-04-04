using AutoMarket.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMarket.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IAutoMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string username) =>
            _users.Find<User>(user => user.Username == username).FirstOrDefault();

        public User Create(User user)
        {
            if (_users.Find<User>(_user => _user.Username == user.Username).FirstOrDefault() != null)
            {
                throw Exception("Username already exists");
            }

            _users.InsertOne(user);
            return user;
        }

        private Exception Exception(string v)
        {
            throw new NotImplementedException();
        }

        public void Update(string username, User userIn)
        {
            _users.ReplaceOne(user => user.Username == username, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Username == userIn.Username);
        }

        public void Remove(string username)
        {
            _users.DeleteOne(user => user.Username == username);
        }
    }
}