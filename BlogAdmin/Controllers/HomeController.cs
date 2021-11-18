using Blog.Data;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    public class HomeController : Controller
    {
        //Yorumdaki bu işlemleri service katmanında yapıcaz
        //private readonly BlogDbContext _blogDbContext;

        //public HomeController(BlogDbContext context)
        //{
        //    // dependency injection: bağımlılıkların runtime'da enjekte edilmesi.net core'da built in olarak gelen bir yapıdır.

        //    // _blogDbContext = new BlogDbContext();
        //    _blogDbContext = context;
        //}
        //public IActionResult Index()
        //{
        //    //var categories = _blogDbContext.Categories.ToList(); // _servis.GetCategories();


        //    return View();
        //}


        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
           // var categories = _categoryRepository.GetCategories();  // new Data.Entities.Category
            return View();
        }
    }
}
