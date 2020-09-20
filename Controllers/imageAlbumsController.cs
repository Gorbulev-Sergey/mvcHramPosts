using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcHramPosts.Data;
using mvcHramPosts.Models;

namespace mvcHramPosts.Controllers
{
    public class imageAlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public imageAlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: imageAlbums
        public async Task<IActionResult> Index()
        {
            //_context.Users.FirstOrDefault(u=>u.UserName==User.Identity.Name).imageAlbums.Add(new imageAlbum {
            //    title="Летом на источнике",
            //    cover_image= "https://sun9-8.userapi.com/c851420/v851420987/17c45b/4N34Y0Ml2nY.jpg",
            //    images=new List<image>
            //    {
            //        new image{url="https://sun9-17.userapi.com/c858032/v858032987/294e7/lwi1k58mg60.jpg" },
            //        new image{url="https://sun9-34.userapi.com/c858428/v858428987/29498/0vjKRNtbLAE.jpg" },
            //        new image{url="https://sun9-51.userapi.com/c854416/v854416987/aaa45/sGe8wx12LoI.jpg" },
            //        new image{url="https://sun9-9.userapi.com/c855728/v855728987/a8001/RL5_4gdAXVE.jpg" },
            //        new image{url="https://sun9-60.userapi.com/c849524/v849524987/1aaad0/Dk2pi56HwWE.jpg" },
            //        new image{url="https://sun9-8.userapi.com/c851420/v851420987/17c45b/4N34Y0Ml2nY.jpg" }
            //    }
            //});
            //_context.SaveChanges();

            return View(await _context.imageAlbums.ToListAsync());
        }

        [HttpPost]
        public IActionResult GetImages(int albumID)
        {
            return PartialView("_images", _context.imageAlbums.Include(i => i.images).FirstOrDefault(a => a.ID == albumID).images.ToList());
        }
    }
}
