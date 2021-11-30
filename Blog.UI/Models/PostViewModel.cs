using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentCount { get; set; }
        public string Title { get; set; }
        public List<Tag> Tags { get; set; }
        public string Content { get; set; }
        public string PictureStr { get; set; }
        public List<Comment> Comments { get; set; }
        public string CategoryName { get; set; }
    }
}
