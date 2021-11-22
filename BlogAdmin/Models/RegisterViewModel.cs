using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Boş geçilemez!")]
        [StringLength(50, ErrorMessage = "Max 50, Min 2 giriş yapılabilir.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Boş geçilemez!")]
        [StringLength(100, ErrorMessage = "Max 100, Min 2 giriş yapılabilir.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Boş geçilemez!")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş geçilemez!")]
        [StringLength(12, ErrorMessage = "Max 12, Min 4 giriş yapılabilir.", MinimumLength = 4)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Boş geçilemez!")]
        [StringLength(12, ErrorMessage = "Max 12, Min 4 giriş yapılabilir.", MinimumLength = 4)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor!")]
        [DisplayName("Password Confirm")]
        public string PasswordConfirm { get; set; }
    }
}
