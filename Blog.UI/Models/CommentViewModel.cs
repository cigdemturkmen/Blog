using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Models
{
    public class CommentViewModel
    {
        [Required]
        [StringLength(500,ErrorMessage ="En fazla 500 karakter girilebilir.")]
        public string CommentText { get; set; }

        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string Nickname { get; set; }
        public int PostId { get; set; }

    }
}
