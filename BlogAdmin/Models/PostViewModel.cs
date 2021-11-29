using Blog.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        public IFormFile Picture { get; set; }

        public string PictureStr { get; set; }

        public bool IsPublished { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<PostTag> PostTags { get; set; }
        public List<Comment> Comments { get; set; }

        public string Tags { get; set; } // 


    }
}
