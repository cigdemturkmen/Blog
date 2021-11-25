using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Services.Concrete
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext blogDbContext)
        {
            _context = blogDbContext;
        }

        public bool Add(Post entity)
        {
            _context.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var post = GetPost(id);
            if (post == null)
            {
                return false;
            }

            post.IsActive = false;
            post.UpdatedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public bool Edit(Post entity)
        {
            var post = GetPost(entity.Id);

            if (post == null)
            {
                return false;
            }

            post.CategoryId = entity.CategoryId;
            post.Title = entity.Title;
            post.Content = entity.Content;
            post.IsPublished = entity.IsPublished;
            post.Picture = entity.Picture;
            post.PostTags = entity.PostTags;
            post.UpdatedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.Where(x => x.IsActive).ToList();
        }
    }
}
