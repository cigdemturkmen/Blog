using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Data.Entities
{
   public class Category : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public byte[] Picture { get; set; } // server içinde file açtırıp cdn üzerinde de tutulabilir. // public string PictureName

        #region Relations
        public List<Post> Posts { get; set; } 
        #endregion
    }
}
