using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mvcHramPosts.Controllers
{
    [Authorize(Roles = "администратор")]
    public class testController : Controller
    {
        public IActionResult image_album()
        {
            return View();
        }

        public IActionResult code_editor()
        {
            return View();
        }

        public IActionResult video()
        {
            return View();
        }
        public IActionResult razmetka_pu()
        {
            return View();
        }

        public IActionResult r1()
        {
            return View();
        }
    }
}