using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mvcHramPosts.Areas.Control.Controllers
{
    [Area("Control"), Authorize(Roles = "администратор, редактор")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Main","Home");
        }

        public IActionResult Main()
        {
            return View();
        }
    }
}