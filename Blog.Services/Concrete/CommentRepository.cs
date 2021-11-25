using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Services.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context)
        {
            _context = context; // ctor injection
        }

        public bool Add(Comment entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            _context.Comments.Add(entity);

            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = GetComment(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool Edit(Comment entity)
        {
            var uptentity = GetComment(entity.Id);
            if (uptentity == null)
            {
                return false;
            }

            uptentity.Nickname = entity.Nickname;
            uptentity.PostId = entity.PostId;
            uptentity.Content = entity.Content;
            uptentity.UpdatedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public Comment GetComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == id && x.IsActive);
            return comment;
        }

        public List<Comment> GetComments()
        {
            var comments = _context.Comments.Where(x => x.IsActive).ToList();
            return comments;
        }
    }
}
