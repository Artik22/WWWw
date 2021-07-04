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

        public IActionResult Index()
        {
            return View(db.Users.ToList());
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
