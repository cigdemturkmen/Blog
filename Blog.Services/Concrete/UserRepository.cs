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
            //entity.Role = "editör";

            _context.Users.Add(entity);

            return _context.SaveChanges() > 0; // bool değer döner
        }

        public bool Delete(int id)
        {
            var entity = GetUser(id);

            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public bool Edit(User entity)
        {
            var updatedEntity = GetUser(entity.Id);

            if (updatedEntity == null)
            {
                return false;
            }

            updatedEntity.Name = entity.Name;
            updatedEntity.Surname = entity.Surname;
            updatedEntity.Email = entity.Email; //değiştirmesini istemiyosan bunu ekleme
            updatedEntity.Password = entity.Password;
            updatedEntity.UpdatedDate = DateTime.Now;
            updatedEntity.Role = entity.Role;

            return _context.SaveChanges() > 0;
        }

        public User GetUser(int id)
        {
           return _context.Users.FirstOrDefault(x => x.Id == id && x.IsActive);

        }

        public List<User> GetUsers()
        {
            return _context.Users.Where(x => x.IsActive).ToList();
        }

        public User Login(string email, string password)
        {
          return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive);
        }
    }
}
