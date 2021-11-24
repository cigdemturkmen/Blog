using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public bool ısPublished { get; set; }
    }
}
