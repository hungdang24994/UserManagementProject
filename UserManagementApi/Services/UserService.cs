using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll(string? status)
        {
            var users = _userRepository.GetAll();

            // LINQ filtering
            if (!string.IsNullOrEmpty(status))
            {
                users = users.Where(u =>
                    u.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            return users;
        }

        public User? GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            _userRepository.Add(user);
            return user;
        }

        public bool UpdateStatus(Guid id, string status)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }

            user.Status = status;
            _userRepository.Update(user);
            return true;
        }
    }
}
