using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Data.Entities
{
   public class Post : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        public byte[] Picture { get; set; }

        public bool IsPublished { get; set; }

        #region Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }

        public List<PostTag> PostTags { get; set; }
        #endregion
    }
}
