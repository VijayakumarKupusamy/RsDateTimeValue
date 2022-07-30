using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Accounts
{
    public class UserRepository
    {
        private readonly Dictionary<int, User> _users;

        public UserRepository()
        {
            _users = new User[]
            {
                new User(1, "Ada Lovelace", new DateTime(1815, 12, 10), "@ada"),
                new User(2, "Alan Turing", new DateTime(1912, 06, 23), "@complete"),
                new User(3, "Megha Clarke", new DateTime(2022, 07, 29), "@Megha"),
                new User(4, "Adam Zampa", new DateTime(2022, 07, 29), "@Adam")
            }.ToDictionary(t => t.Id);
        }

        public User GetUser(int id) => _users[id];

        public IEnumerable<User> GetUsers() => _users.Values;

        //public IEnumerable<User> GetUsersByBirthDate(DateTime birthDate)
        //{
        //    return _users.Values.Where(x=>x.Birthdate==birthDate);
        //}
        public IEnumerable<User> GetUsersByBirthDate(User user)
        {
            return _users.Values.Where(x => x.Birthdate == user.Birthdate);
        }
    }
}