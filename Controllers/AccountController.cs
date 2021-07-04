using web.Models;
using web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace web.Controllers
{
    public class AccountController : Controller
    {
        private Context db;
        public AccountController(Context context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //  User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                User user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                      await Authenticate(user); // аутентификация
                  //  await Authenticate(user.ToString());
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                     user = new User { Email = model.Email, Password = model.Password };
                    // добавляем пользователя в бд
                   // db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.RoleName == "user");
                    if (userRole != null)
                     
                        user.Roles.Add(userRole);
                        db.Users.Add(user);

                    await db.SaveChangesAsync();
                  //  await Authenticate(model.Email); // аутентификация

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
             //   new Claim(ClaimsIdentity.DefaultNameClaimType, userName.Email),
                 new Claim(ClaimsIdentity.DefaultNameClaimType, userName.Email),
             //    new Claim(ClaimsIdentity.DefaultRoleClaimType, userName.Roles.ToString())
             new Claim(ClaimsIdentity.DefaultRoleClaimType, userName.Roles.First().RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }




    
        //[HttpGet]
        //public async Task<IActionResult> EdiAccount(int? id)
        //{
        //    if (id != null)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
        //        if (user != null)
        //            return View(user);
        //    }
        //    return NotFound();
        //}




       


    }
}
