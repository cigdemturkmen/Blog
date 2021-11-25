using Blog.Data.Entities;
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
    public class UserController : BaseController
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
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role,
            }).ToList();

            //var users = _userRepository.GetUsers(); olmadı :P çünkü cshtml sayfası viewmodeli base alarak oluşturulduğu için.
            return View(users);
        }

        public IActionResult Add()
        {
            return View("AddOrEdit");
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user!= null)
            {
                var vm = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Password = user.Password,
                    Role = user.Role,
                };
                return View("AddOrEdit", vm);
            }

            TempData["Message"] = "Güncellenecek kullanıcı bulunamadı.";

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(UserViewModel model)
        {
            if (model.Id == 0)
            {
                if (ModelState.ContainsKey("Id"))
                    ModelState.Remove("Id");

            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Id = model.Id,
                Name =model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            var currentUserId = GetCurrentUserId();
            bool result;

            if (model.Id == 0)
            {
                user.CreatedById = currentUserId;
                result = _userRepository.Add(user);
            }
            else
            {
                user.UpdatedById = currentUserId;
                result = _userRepository.Edit(user);
            }

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model); // AddOrEdit model ;)
        }

        public IActionResult Delete(int id)
        {
            var result = _userRepository.Delete(id);

            TempData["Message"] = result ? "Silindi" : "İşlem başarısız oldu.";
            return RedirectToAction("List");
        }
    }
}
