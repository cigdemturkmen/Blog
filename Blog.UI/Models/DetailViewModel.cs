using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Models
{
    public class DetailViewModel
    {
        public PostViewModel PostDetail { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}
