using Blog.Data.Entities;
using Blog.Services.Interfaces;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Category category = new Category()
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedById = -1,
            };

            #region Picture için düzenleme.
            //viewmodelden gelen pic türü ile entity'imdeki pic türü uymuyo o yüzden...picture'ı byte[]e çeviriyoruz

            if (model.Picture.Length > 0) // length 0 ise dosyanın içi boştur
            {
                using (var ms = new MemoryStream()) //memory streamda bir yer açıyo
                {
                    model.Picture.CopyTo(ms);
                    var fileByteArray = ms.ToArray();

                    category.Picture = fileByteArray;
                }
            }
            else
            {
                ViewBag.Message = "boş dosya yükleyemezsiniz";
            }
            #endregion
           var sonuc = _categoryRepository.Add(category);
            if (sonuc)
            {
                return RedirectToAction("List");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
