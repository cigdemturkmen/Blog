using Blog.Data.Entities;
using Blog.Services.Interfaces;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Name = model.FirstName,
                Surname = model.LastName,
                Email = model.Email,
                Password = model.Password,
                CreatedById = -1,
                Role = "editor",
            };

            var result = _userRepository.Add(user);

            if (result)
            {
                return RedirectToAction("Login");
            }

            // mesaj
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // user db'de var mı?
            var user = _userRepository.Login(model.Email, model.Password);

            if (user != null)
            {
                // varsa authentication yapılacak

                // Authentication/Authorization (oturum açık kapalı/yetki durumu) için
                // 1.startup.cs'de auth mekanizmasını eklemek
                // 2.login içinde authenticated yapmak

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname,user.Surname),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()), // Id
                    new Claim(ClaimTypes.Role, user.Role),


                }; // user'ın bazı bilgilerini tutacağımız şey.liste halinde
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login"); // key login, valueları claims.
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal); /* async metod görüyosam başına await yazıyorum. metodu da asyn olarak düzenlememiz gerekiyor (login'in post'una bak task onun async old. söyleyen şeylerden biri.) signin metodu: .... */

                return RedirectToAction("Index", "Home");
            }


            ViewBag.Message = "Kullanıcı adınızı veya şifrenizi kontrol ediniz.";
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
