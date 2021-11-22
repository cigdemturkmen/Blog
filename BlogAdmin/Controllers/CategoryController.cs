﻿using Blog.Data.Entities;
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
            var categories = _categoryRepository.GetCategories().Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                PictureStr = Convert.ToBase64String(x.Picture)
            }).ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    return View("Add", model);
                }
                else
                {
                    return View("Edit", model);
                }
                //return View(model);
            }

            Category entity = new Category()
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

                    entity.Picture = fileByteArray;
                }
            }
            else
            {
                ViewBag.Message = "boş dosya yükleyemezsiniz";
            }
            #endregion

            bool result;

            if (model.Id == 0)
            {
                // new
                result = _categoryRepository.Add(entity);
            }
            else
            {
                // edit
                entity.Id = model.Id;
                result = _categoryRepository.Edit(entity);
            }
            
            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category != null)
            {
                var viewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    Description = category.Description,

                };
                return View(viewModel);
            }
            // mesaj
            return RedirectToAction("List");
        }

        //[HttpPost]
        //public IActionResult Edit(CategoryViewModel model)
        //{
        //    return View();
        //}

        public IActionResult Delete(int id)
        {
            var result = _categoryRepository.Delete(id);

            TempData["Message"] = result ? "İşlem başarılı" : "Silme işlemi yapılmadı";
            return RedirectToAction("List");
        }
    }
}
