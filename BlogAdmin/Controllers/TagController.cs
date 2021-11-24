using Blog.Data.Entities;
using Blog.Services.Interfaces;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

       
        public IActionResult List()
        {
            var tags = _tagRepository.GetTags().Select(x => new TagViewModel
            {
                Name = x.Name,
                Id = x.Id,
            }
            );

            return View(tags);
        }

        public IActionResult Add()
        {
            return View();
        }


        public IActionResult Edit(int id)
        {
            var tag = _tagRepository.GetTag(id);

            if (tag != null)
            {
                var viewModel = new TagViewModel()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                };

                return View(viewModel);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(TagViewModel model)
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
            }

            //var currentUserIdStr = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //var currentuserId = Convert.ToInt32(currentUserIdStr);

            var currentUserId = GetCurrentUserId();

            Tag entity = new Tag()
            {
                Id = model.Id, // bunu yazmasan da olur
                Name = model.Name,
                
            };

            bool result;

            if (model.Id == 0)
            {
                // new
                entity.CreatedById = currentUserId;
                result = _tagRepository.Add(entity);
            }
            else
            {
                // edit
                entity.Id = model.Id;
                entity.UpdatedById = currentUserId;
                result = _tagRepository.Edit(entity);
            }

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
           
        }


        public IActionResult Delete(int id)
        {
            var result = _tagRepository.Delete(id);

            TempData["Message"] = result ? "İşlem başarılı" : "Silme işlemi yapılmadı";
            return RedirectToAction("List");
        }
    }
}
