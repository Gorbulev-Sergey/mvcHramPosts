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
            return View(await _context.imageAlbums.ToListAsync());
        }

        [HttpPost]
        public IActionResult GetImages(int albumID)
        {
            return PartialView("_images", _context.imageAlbums.Include(i => i.images).FirstOrDefault(a => a.ID == albumID).images.ToList());
        }

        // GET: imageAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageAlbum = await _context.imageAlbums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (imageAlbum == null)
            {
                return NotFound();
            }

            return View(imageAlbum);
        }

        // GET: imageAlbums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: imageAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,title,description,cover_image,created,updated,userId,postId")] imageAlbum imageAlbum, List<string> images_urls, List<string> images_titles)
        {
            if (ModelState.IsValid)
            {
                imageAlbum.userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
                imageAlbum.created = imageAlbum.updated = DateTime.Now;

                for (int i = 0; i < images_urls.Count; i++)
                {
                    if (!string.IsNullOrEmpty(images_urls[i]))
                    {
                        imageAlbum.images.Add(new image
                        {
                            url = images_urls[i],
                            title = images_titles[i],
                            updated = DateTime.Now
                        });
                    }
                }

                _context.Add(imageAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageAlbum);
        }

        // GET: imageAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageAlbum = await _context.imageAlbums.Include(i => i.images).FirstOrDefaultAsync(a => a.ID == id);
            if (imageAlbum == null)
            {
                return NotFound();
            }
            return View(imageAlbum);
        }

        // POST: imageAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,title,description,cover_image,created,updated,userId,postId,images")] imageAlbum imageAlbum, List<string> images_urls, List<string> images_titles)
        {
            if (id != imageAlbum.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    imageAlbum.updated = DateTime.Now;
                    for (int i = 0; i < images_urls.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(images_urls[i]))
                        {
                            imageAlbum.images.Add(new image
                            {
                                url = images_urls[i],
                                title = images_titles[i],
                                updated = DateTime.Now
                            });
                        }
                    }

                    _context.Update(imageAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!imageAlbumExists(imageAlbum.ID))
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
            return View(imageAlbum);
        }

        // GET: imageAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageAlbum = await _context.imageAlbums
                .FirstOrDefaultAsync(m => m.ID == id);
            if (imageAlbum == null)
            {
                return NotFound();
            }

            return View(imageAlbum);
        }

        // POST: imageAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageAlbum = await _context.imageAlbums.Include(i => i.images).FirstOrDefaultAsync(a => a.ID == id);
            _context.imageAlbums.Remove(imageAlbum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool imageAlbumExists(int id)
        {
            return _context.imageAlbums.Any(e => e.ID == id);
        }
    }
}