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
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace web.Controllers
{
    //  [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private Context db;
        public ArticleController(Context contex)
        {
            db = contex;

        }

        //[Authorize]
        public ActionResult Index()//сотрудники компании
        {

            return View();
        }

        public async Task<IActionResult> Index(int? id)
        {

            if (id != null)
            {
                //  Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                User article = await db.Users.Include(c => c.Articles).FirstOrDefaultAsync(p => p.Id == id);
                if (article != null)
                {
                    List<Article> users = article.Articles.ToList();
                    return View(article);
                }
            }
            return NotFound();



            //return View(db.Users.ToList());dd
            //return View(db.Articles.ToList());
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateArticl()
        {
            ViewBag.Groups = new SelectList(db.Groups, "GroupId", "GroupName");
            //  ViewBag.Users = new SelectList(db.Users, "Id", "FirsName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateArticl(int GroupId, string name, string content1)// string users   или  int usersId
        {
            Article article = new Article { Title = name, Description = content1, DateOfCreate = DateTime.Now, DateOfEdit = DateTime.Now };
            Group group = await db.Groups.FirstOrDefaultAsync(d => d.GroupId == GroupId);
            Position userPosition = await db.Positions.FirstOrDefaultAsync(p => p.PositionName == "Admin"); //Roles.FirstOrDefaultAsync(r => r.RoleName == "user");

            //db.Articles.Add(userPosition);
            //db.Groups.Add(userPosition);
            article.Groups.Add(group);
            article.Positions.Add(userPosition);
            db.Articles.Add(article);

            await db.SaveChangesAsync();

            return RedirectToAction("ListTitleArticl");
        }

       [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArticl(int? id)
        {
            if (id != null)
            {
                Article user = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArticl(int id, int GroupId, string name, string content1)
        {
            
            if (id != null)
            {
                Article article = new Article {Id = id, Title = name, Description = content1, DateOfCreate = DateTime.Now, DateOfEdit = DateTime.Now };
                //Group group = await db.Groups.FirstOrDefaultAsync(d => d.GroupId == GroupId);

                db.Articles.Update(article);
                await db.SaveChangesAsync();
                //Article articl = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
                //if (articl != null)

                return RedirectToAction("ListTitleArticl");
            }
            return NotFound();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArticl(int? id)
        {
            Article user = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
            if (id != null)
            {
                db.Articles.Remove(user);
                db.SaveChangesAsync();
                return RedirectToAction("ListTitleArticl");
            }
            return RedirectToAction("ListTitleArticl");
        }




        public async Task<IActionResult> ListTitleArticl()//int? id
        {
            Claim us = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);
            if (us == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string role = us.Value;

            //return Content($"ваша роль: { role.ToString() }");


            string EmailContex = HttpContext.User.Identity.Name;  //получили емаил из куки

            User user = await db.Users.Include(c => c.Articles).FirstOrDefaultAsync(p => p.Email == EmailContex);
          //  Article article1 = await db.Articles.FirstOrDefaultAsync(a => a.Users == user);
            //  return View(article1);

            if (user != null)
            {
                List<int> ids = new List<int>();
                List<Article> aricles = user.Articles.ToList();
                foreach (var article in aricles)
                {
                    ids.Add(article.Id);
                }
             //   var groups = db.Groups.FirstOrDefaultAsync(p => p.Positions == user.Positions);
               // var groups = db.Groups.Include(c => c.Articles).Where(g => ids.Contains(g.GroupId)).ToList();
                ViewBag.user = role.ToString();
                return View(db.Articles.ToList());

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> CreateGruppList()
        {
             return View(db.Groups.ToList());
          //  return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateGruppList(string nameGroup)
        {

            Group group = new Group {GroupName = nameGroup};
            Position position = await db.Positions.FirstOrDefaultAsync(p => p.PositionName == "Admin");
            group.Positions.Add(position);
            db.Groups.Add(group);
            await db.SaveChangesAsync();
            
            // ViewBag.Users = new (users, "Id", "Name");
            return RedirectToAction("ListTitleArticl");



         //   return View();
        }




        //document output
        public async Task<IActionResult> DocumentOutput(int? id)
        {

          Article article = await db.Articles.FirstOrDefaultAsync(p => p.Id == id);
             
            if (article != null)
                {
                    return View(article);
                }
           return NotFound();
        }


    }
}
