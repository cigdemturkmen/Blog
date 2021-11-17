using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Entities
{
   public class PostTag
    {
        // EF Core'da [Key] ara tablolardki keyleri belirtmek için OnModelCreating'e fluent olarak yazılması gerekir. 

        public int PostId { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public Post Post { get; set; }
    }
}
