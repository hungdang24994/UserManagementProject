using System;
using System.Collections.Generic;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(Guid id);
        void Add(User user);
        void Update(User user);
    }
}
