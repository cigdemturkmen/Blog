using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blog.Services.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _context;

        public CategoryRepository(BlogDbContext context)
        {
            _context = context;
        }

        public bool Add(Category entity)
        {
            // BLL + DAL (bu kodları da burada yazabiliriz)
            // entity.IsActive = true; zaten startupta default değeri veridli.
            // entity.CreatedDate = DateTime.Now;
            // ya da iş kodları
            _context.Categories.Add(entity);

            var result = _context.SaveChanges() > 0;
            return result;
        }

        public bool Delete(int id)
        {
            var entity = GetCategory(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool Edit(Category entity)
        {
            var category = GetCategory(entity.Id);
            if (category != null)
            {
                category.CategoryName = entity.CategoryName;
                category.Description = entity.Description;
                category.Picture = entity.Picture;
                category.Posts = entity.Posts;
                category.UpdatedDate = DateTime.Now;

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories.Where(x => x.IsActive).ToList();
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id && x.IsActive);
            return category;
        }
    }
}
