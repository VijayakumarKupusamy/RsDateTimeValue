using System;
using System.Collections.Generic;
using HotChocolate;

namespace Demo.Accounts
{
    public class Query
    {
        public IEnumerable<User> GetUsers([Service] UserRepository repository) =>
            repository.GetUsers();

        public User GetUser(int id, [Service] UserRepository repository) => 
            repository.GetUser(id);

        //public IEnumerable<User> GetUsersByBirthDate(DateTime birthDate,[Service] UserRepository repository) =>
        //   repository.GetUsersByBirthDate(birthDate);

        public IEnumerable<User> GetUsersByBirthDate(User user, [Service] UserRepository repository) =>
          repository.GetUsersByBirthDate(user);

    }
}