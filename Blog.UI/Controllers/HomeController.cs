using Blog.Services.Interfaces;
using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;

        public HomeController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            #region Kategoriler bölümü için
            var categories = _categoryRepository.GetCategories() // x- categories. y- posts
                    .Select(x => new CategoryViewModel()
                    {
                        CategoryName = x.CategoryName,
                        Id = x.Id,
                        PostCount = x.Posts.Count(y => y.IsActive),
                    }).ToList();

            ViewBag.Categories = categories;
            #endregion

            #region Postlar için

            var posts = _postRepository.GetLatest5Posts()
                .Select(x => new PostViewModel()
                {
                    Title = x.Title,
                    Content = x.Content,
                    CommentCount = x.Comments.Count(y => y.IsActive && y.IsPublished),
                    CreatedDate = x.CreatedDate,
                    PictureStr = Convert.ToBase64String(x.Picture),
                    Tags = x.PostTags.Select(y => y.Tag).ToList()
                }).ToList();

            ViewBag.Posts = posts;
            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
