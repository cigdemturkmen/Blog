using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        [StringLength(10)]
        public string Role { get; set; }
    }
}
