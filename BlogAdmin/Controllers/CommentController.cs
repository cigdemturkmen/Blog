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
    public class CommentController : BaseController
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IActionResult List()
        {
            var comments = _commentRepository.GetComments().Select(x => new CommentViewModel()
            {
                Id = x.Id,
                Nickname = x.Nickname,
                Content = x.Content,

            }).ToList();
            return View(comments);
        }

        public IActionResult Edit(int id)
        {
            var comment = _commentRepository.GetComment(id);
            if (comment != null)
            {
                var viewModel = new CommentViewModel()
                {
                    Id = comment.Id,
                    Nickname = comment.Nickname,
                    Content = comment.Content

                };
                return View(viewModel);
            }
            // mesaj
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var currentUserIdStr = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var currentuserId = Convert.ToInt32(currentUserIdStr);

            Comment entity = new Comment()
            {
                

            };

            bool result;

            if (model.Id == 0)
            {
                // new
                entity.CreatedById = currentuserId;
                result = _commentRepository.Add(entity);
            }
            else
            {
                // edit
                entity.Id = model.Id;
                entity.UpdatedById = currentuserId;
                result = _commentRepository.Edit(entity);
            }

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
