using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Boş geçilemez!")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş geçilemez!")]
        [StringLength(12, MinimumLength = 4 ,ErrorMessage = "En az 4 en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
