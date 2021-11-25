using Blog.Data.Entities;
using Blog.Services.Interfaces;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }


        public ActionResult Add()
        {
            
            ViewBag.Categories = _categoryRepository.GetCategories().Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();

            return View("AddOrEdit");
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetCategories().Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            var post = new Post()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Content = model.Content,
                Title = model.Title,
            };

            #region Picture
            if (model.Picture.Length > 0)
            {
                using(var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    post.Picture = ms.ToArray();
                }
            }
            #endregion

            var currentUserId = GetCurrentUserId();

            bool result;

            if (model.Id == 0)
            {

            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
