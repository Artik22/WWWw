using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Models;
using web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private Context db;
        // GET: CompanyController
        public  CompanyController(Context contex)
        {
            db = contex;
         //   return View();
        }

        [Authorize]
        public ActionResult UserStaff()//сотрудники компании
        {
     
            return View(db.Users.ToList());
        }





        //[Authorize]
        //[HttpPost]
        //public ActionResult AddUserStaff( model)//сотрудники компании
        //{
        //    return View();
        //}
        [Authorize]
        [HttpGet]
        public IActionResult AddUserStaff()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserStaff(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                
                    return RedirectToAction("UserStaff");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }







        public ActionResult Structure()//структура компании
        {
            return View();
        }

    }
}
