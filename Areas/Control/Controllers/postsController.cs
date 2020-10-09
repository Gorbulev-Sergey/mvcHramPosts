using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcHramPosts.Data;
using mvcHramPosts.Models;

namespace mvcHramPosts.Areas.Control.Controllers
{
    [Area("Control"), Authorize(Roles = "администратор")]
    public class postsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public postsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: posts
        public async Task<IActionResult> Index()
        {
            ViewBag.context = _context;
            return View(_context.posts.Include(p => p.comments).Include(l => l.likes).ToList());
        }

        // GET: posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.posts.FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,title,cover_image,description,content,type,created,updated,userId,published")] post post)
        {

            if (ModelState.IsValid)
            {
                post.userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
                post.created = post.updated = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,title,cover_image,description,content,type,created,updated,userId,published")] post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.updated = DateTime.Now;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!postExists(post.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.posts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Здесь Include нужно для того, чтобы включить каскадное удаление, то есть удалить не только пост,
            // но и входящие в него комментарии, лайки, фотографии и видеозаписи
            var post = await _context.posts
                .Include(p => p.comments).Include(p => p.images).Include(p => p.likes).Include(p => p.videos)
                .FirstOrDefaultAsync(p => p.ID == id);
            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool postExists(int id)
        {
            return _context.posts.Any(e => e.ID == id);
        }
    }
}