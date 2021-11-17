using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Data.Entities
{
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #region Relations
        public List<PostTag> PostTags { get; set; }
        #endregion
    }
}
