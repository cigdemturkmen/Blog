using Blog.Data;
using Blog.Data.Entities;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        private void CreateTags(Post entity)
        {
            #region Taglerin eklenmesi
            // PostTag > Tag > Name

            // Db'de varsa bunun Id'sinin alınması ve PostTag > TagId!e yazılması gerekiyor
            // Eğer etiket dbde yoksa yeni kayıt olarak eklenmesi lazım

            var allTags = _context.Tags.ToList();

            var tagList = new List<PostTag>();

            foreach (var item in entity.PostTags)
            {
                // item.Tag.Name // dbde varlığı kontrol edilecek isim
                var tag = allTags.FirstOrDefault(x => x.Name == item.Tag.Name);
                if (tag != null)
                {
                    // daha önce bu tag kayıt edilmiştir
                    tagList.Add(new PostTag() { TagId = tag.Id });
                }
                else
                {
                    // tag yeni kayıt olarak eklenmeli
                    tagList.Add(new PostTag() { Tag = item.Tag });
                }
            }

            entity.PostTags = tagList;

            #endregion
        }

        public bool Add(Post entity)
        {
            //#region Tag'lerin eklenmesi

            //// PostTag > Tag > Name
            //// Db'de varsa bunun Id'sinin alınması ve PostTag > TagId'e yazılması gerekiyor.
            //// eğer etiket db'de yoksa yeni kayıt olarak eklenmesi lazım
            //var allTags = _context.Tags.ToList();
            //var tagList = new List<PostTag>();
            //foreach (var item in entity.PostTags)
            //{
            //    // item.Tag.PostTags // db'de varlığı kontrol edilecek isim.
            //    var tag = allTags.FirstOrDefault(x => x.Name == item.Tag.Name);

            //    if (tag != null)
            //    {
            //        //daha önce bu tag kayıt edilmiştir.
            //        tagList.Add(new PostTag() { TagId = tag.Id });
            //    }
            //    else
            //    {
            //        // tag yeni kayıt olarak eklenmeli.
            //        tagList.Add(new PostTag() { Tag = item.Tag });
            //    }
            //}
            //entity.PostTags = tagList;

            //#endregion

            CreateTags(entity);

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

            CreateTags(entity);

            return _context.SaveChanges() > 0;
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(x => x.Category)
                .Include(x => x.PostTags).ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.Include(x=> x.Category).Where(x => x.IsActive).ToList();
        }

        public List<Post> GetLatest5Posts()
        {
           return _context.Posts.Include(x => x.Comments).Include(x => x.PostTags).ThenInclude(x => x.Tag)
                .Where(x => x.IsActive && x.IsPublished)
                .OrderByDescending(x => x.CreatedDate)
                .Take(5) // select top 5
                .ToList();
        }
    }
}
