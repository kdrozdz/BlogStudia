using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BlogProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BlogPostModel.Include(b => b.Author);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        [Authorize(Roles = "Admin, Basic")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.BlogPostModel
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostModel == null)
            {
                return NotFound();
            }

            return View(blogPostModel);
        }

        [Authorize(Roles = "Admin, Basic")]
        public IActionResult SearchByTag(BlogPostTag tag)
        {
            ViewBag.Tags = _context.BlogPostModel
            .Select(b => b.Tag)
            .Distinct()
            .ToList();
            return View(_context.BlogPostModel.Where(j => j.Tag == tag).ToList());
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin, Basic")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Basic")]
        public async Task<IActionResult> Create([Bind("Id,CreatedAt,Title,Content,AuthorId,Tag")] BlogPostModel blogPostModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            blogPostModel.Author = user;
            blogPostModel.AuthorId = userId;
            blogPostModel.CreatedAt = DateTime.Now;
            blogPostModel.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(blogPostModel);
                var res = await _context.SaveChangesAsync();
                return new RedirectResult(url: "/", permanent: true, preserveMethod: true);
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.AuthorId);
            return View(blogPostModel);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.BlogPostModel.FindAsync(id);
            if (blogPostModel == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.AuthorId);
            return View(blogPostModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CreatedAt,Title,Content,AuthorId")] BlogPostModel blogPostModel)
        {
            if (id != blogPostModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPostModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostModelExists(blogPostModel.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.AuthorId);
            return View(blogPostModel);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.BlogPostModel
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostModel == null)
            {
                return NotFound();
            }

            return View(blogPostModel);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var blogPostModel = await _context.BlogPostModel.FindAsync(id);
            if (blogPostModel != null)
            {
                _context.BlogPostModel.Remove(blogPostModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostModelExists(string id)
        {
            return _context.BlogPostModel.Any(e => e.Id == id);
        }
    }
}
