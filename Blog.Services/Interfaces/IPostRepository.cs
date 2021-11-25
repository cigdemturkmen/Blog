using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetPosts();
        Post GetPost(int id);
        bool Add(Post entity);
        bool Edit(Post entity);
        bool Delete(int id);


    }
}
