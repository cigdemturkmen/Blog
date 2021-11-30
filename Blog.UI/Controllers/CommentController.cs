using Blog.Services.Interfaces;
using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController()
        {

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CommentViewModel model)
        {
            return View();
        }
    }
}
