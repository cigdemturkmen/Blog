using Blog.Services.Interfaces;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult List()
        {
            var users = _userRepository.GetUsers().Select(x => new UserViewModel()
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role,
            }
            );

            //var users = _userRepository.GetUsers(); olmadı :P çünkü cshtml sayfası viewmodeli base alarak oluşturulduğu için.
            return View(users);
        }
    }
}
