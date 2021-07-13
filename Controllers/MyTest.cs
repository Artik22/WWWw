using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace web.Controllers
{
    public class MyTest : Controller
    {

        private Context db;
        public MyTest(Context contex)
        {
            db = contex;
            //   return View();
        }


        public string GetContext()
        {
            string brouser = HttpContext.User.Identity.Name;

            return "пользователь" + brouser;// + "пользователь" + brouser1 + "пользователь" + brouser11;
        }

        public async Task<IActionResult> Index()
        {
           

            Claim us = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);
            if (us == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string role = us.Value;

            //return Content($"ваша роль: { role.ToString() }");


            string EmailContex = HttpContext.User.Identity.Name;  //получили емаил из куки



            User user1 = await db.Users.FirstOrDefaultAsync(p => p.Email == EmailContex);

            //получить ид присвоить и выдать 
            Group user = await db.Groups.Include(c => c.Articles).FirstOrDefaultAsync(p => p.GroupId == 2);
          
            Group user2 = await db.Groups.FirstOrDefaultAsync(p => p.Users == user1);
            //передаем в представление список
            var a = user.Articles.ToList();
        
            
            
            
                Article article1 = await db.Articles.FirstOrDefaultAsync(a => a.Users == user1);
            // return View(article1);
         //   Group group = await db.Groups.Include(a => a.Articles).FirstOrDefaultAsync(d => d.Users == EmailContex);
            if (user != null)
            {
                List<int> ids = new List<int>();
                //  List<Article> aricles = user.Articles.ToList();
                List<Article> groups1 = user.Articles.ToList();
                foreach (var article in groups1)
                {
                    ids.Add(article.Id);
                }
                //   var groups = db.Groups.FirstOrDefaultAsync(p => p.Positions == user.Positions);
                //  var groups = db.Groups.Include(c => c.Articles).Where(g => ids.Contains(g.GroupId)).ToList();
                ViewBag.user = role.ToString();
                return View(a);

            }
            return NotFound();

        }

        public ActionResult Details(int id)
        {
            User student;//= db.Users.Find(id);
            student = db.Users.Include(s => s.Articles).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return View();
            }
            return View(student);
        }

        public ActionResult Edit(int id = 0)
        {
            User student = db.Users.Find(id);
            if (student == null)
            {
                return View();
            }
            ViewBag.Articles = db.Users.ToList();
            return View(student);
        }
        
      
         


        [HttpPost]
        public ActionResult Edit(User student, int[] selectedCourses)
        {
            User newStudent = db.Users.Find(student.Id);
            newStudent.FirsName = student.FirsName;
            newStudent.LastName = student.LastName;

            newStudent.Articles.Clear();
            if (selectedCourses != null)
            {
                //получаем выбранные курсы
                foreach (var c in db.Articles.Where(co => selectedCourses.Contains(co.Id)))
                {
                    newStudent.Articles.Add(c);
                }
            }

            db.Entry(newStudent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> CreateGruppList()
        {
            //   return View(db.Groups.ToList());
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateGruppList(string nameGroup)
        {

            Group group = new Group { GroupName = nameGroup };
            Position position = await db.Positions.FirstOrDefaultAsync(p => p.PositionName == "Admin");
            group.Positions.Add(position);
            db.Groups.Add(group);
            await db.SaveChangesAsync();

            // ViewBag.Users = new (users, "Id", "Name");
            return RedirectToAction("ListTitleArticl");



            //   return View();
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(string name , string content1)
        //{
        //    db.Articles.Add(new Article { Title = name, Description = content1 });//.Add( new Bs { Title = "1", Description = content1 });
        //   await db.SaveChangesAsync();
        //    //db.SaveChanges();
        //       ViewBag.Content1 = content1;
        //    return View();
        //}

        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        //        if (user == null)
        //        {
        //            // добавляем пользователя в бд
        //            db.Users.Add(new User { Email = model.Email, Password = model.Password });
        //            await db.SaveChangesAsync();

        //            await Authenticate(model.Email); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}
    }
}
