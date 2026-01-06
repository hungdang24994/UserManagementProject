using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementApi.Data;
using UserManagementApi.Models;

namespace UserManagementApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }   

        public User? GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
