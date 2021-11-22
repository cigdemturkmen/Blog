using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="required!")]
        [StringLength(30,ErrorMessage ="max 30")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "required!")]
        [StringLength(500, ErrorMessage = "max 500")]
        public string Description { get; set; }

        [Required(ErrorMessage = "required!")]
        public IFormFile Picture { get; set; }  //kullanıcıdan resim alacaksan.. IFormFile türünden. input type =file UI tarafında. 

        public string PictureStr { get; set; }

    }
}
