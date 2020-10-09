using Microsoft.EntityFrameworkCore;
using mvcHramPosts.Data;
using mvcHramPosts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcHramPosts.Areas.Control.Services
{
    public interface IPostsService
    {
        Task<List<post>> posts();
        Task add(post post);
    }

    public class PostsService : IPostsService
    {
        ApplicationDbContext _context;
        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task add(post post)
        {
            await _context.posts.AddAsync(post);
        }

        public async Task<List<post>> posts()
        {
            return await _context.posts.ToListAsync();
        }
    }
}
