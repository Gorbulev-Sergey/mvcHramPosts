using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcHramPosts.Data;
using mvcHramPosts.Models;

namespace mvcHramPosts.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //_context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).posts.Add(new post
            //{
            //    title = "Всем привет",
            //    content = "Пора гладить",
            //    short_content = "Пора спать",
            //    cover_image = "https://mediatrend.mediamarkt.com.tr/wp-content/uploads/2017/08/Intel-Core-i9-X-series-Skylake.jpg"
            //});
            //_context.SaveChanges();
            ViewBag.context = _context;

            return View(_context.posts.Include(p => p.comments).Include(l => l.likes).ToList());
        }

        [HttpPost]
        public IActionResult AddComment(comment comment)
        {
            comment.userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            comment.userName = User.Identity.Name;
            var comments = _context.posts.Include(c => c.comments).FirstOrDefault(p => p.ID == comment.postID).comments;
            comments.Add(comment);
            _context.SaveChanges();

            return PartialView("_comments", comments);
        }

        [HttpPost]
        public IActionResult AddLike(like like)
        {
            string button;
            like.userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;

            // Проверяем есть ли лайк в базе данных            
            if (_context.posts.Include(l => l.likes).FirstOrDefault(p => p.ID == like.postID).likes.FirstOrDefault(l => l.userId == like.userId && l.postID == like.postID) == null)
            {
                // Добавляем лайк
                _context.posts.FirstOrDefault(p => p.ID == like.postID).likes.Add(like);
                _context.SaveChanges();
                button = "<i class='far fa-heart text-danger font-weight-bold'></i>";
            }
            else
            {
                // Убираем лайк
                _context.likes.Remove(_context.likes.FirstOrDefault(l => l.postID == like.postID && l.userId == like.userId));
                _context.SaveChanges();
                button = "<i class='far fa-heart'></i>";
            }

            //var count = _context.posts.Include(l=>l.likes).FirstOrDefault(p => p.ID == like.postID).likes.Count();

            return Content(button);
        }

        public IActionResult TimeTable()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewBag.context = _context;
            return View(_context.posts.FirstOrDefault(p=>p.ID==id));
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
