using System;
using System.Collections.Generic;
using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll(string? status);
        User? GetById(Guid id);
        User Create(User user);
        bool UpdateStatus(Guid id, string status);
    }
}
