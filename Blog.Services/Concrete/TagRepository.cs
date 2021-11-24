using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Services.Concrete
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _context;

        public TagRepository(BlogDbContext context)
        {
            _context = context;
        }

        public bool Add(Tag entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;

            _context.Tags.Add(entity);

            var result = _context.SaveChanges() > 0;
            return result;
        }

        public bool Delete(int id)
        {
            var entity = GetTag(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool Edit(Tag entity)
        {
            var tag = GetTag(entity.Id);
            if (tag != null)
            {
                tag.Name = entity.Name;
                tag.PostTags = entity.PostTags; // zorunlu değil
                tag.UpdatedDate = DateTime.Now;

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public Tag GetTag(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id && x.IsActive);
            return tag;
        }

        public List<Tag> GetTags()
        {
            var tags = _context.Tags.Where(x => x.IsActive).ToList();
            return tags;
        }
    }
}
