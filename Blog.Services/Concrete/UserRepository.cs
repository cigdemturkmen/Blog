using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Services.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext blogDbContext)
        {
            _context = blogDbContext;
        }

        public bool Add(User entity)
        {
            if (_context.Users.Any(x => x.Email == entity.Email)) // iş kuralı
            {
                return false;
            }

            entity.CreatedDate = DateTime.Now;

            _context.Users.Add(entity);

            return _context.SaveChanges() > 0; // bool değer döner
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
