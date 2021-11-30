using Blog.Services.Interfaces;
using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public IActionResult Detail(int id)
        {
            var postDetail = _postRepository.GetPost(id);

            var postDetailVm = new PostViewModel()
            {
                Title = postDetail.Title,
                Content = postDetail.Content,
                PictureStr = Convert.ToBase64String(postDetail.Picture),
                CommentCount = postDetail.Comments.Count,
                CreatedDate = postDetail.CreatedDate,
                Id = postDetail.Id,
                Tags = postDetail.PostTags.Select(x => x.Tag).ToList(),
                Comments = postDetail.Comments,
                CategoryName = postDetail.Category.CategoryName,

            };

            var vm = new DetailViewModel()
            {
                PostDetail = postDetailVm,
                Comment = new CommentViewModel()
            };

            return View(vm);
        }
    }
}
