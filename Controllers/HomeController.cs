using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
                //  return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }
        
        
       //[Authorize(Roles = "Admin")]
       // public IActionResult About()
       // {
       //     string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

       //     return Content($"ваша роль: { role.ToString() }");
       //     //return Content("Authorized");
       // }






    }
}
