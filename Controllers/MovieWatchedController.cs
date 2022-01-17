#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L8HandsOn.Data;
using L8HandsOn.Models;

namespace L8HandsOn.Controllers
{
    public class MovieWatchedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieWatchedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieWatched
        public async Task<IActionResult> Index()
        {
            return View(await _context.WatchedMovies.ToListAsync());
        }

        // GET: MovieWatched/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieWatched = await _context.WatchedMovies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieWatched == null)
            {
                return NotFound();
            }

            return View(movieWatched);
        }

        // GET: MovieWatched/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieWatched/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WatcherId,Title")] MovieWatched movieWatched)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieWatched);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieWatched);
        }

        // GET: MovieWatched/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieWatched = await _context.WatchedMovies.FindAsync(id);
            if (movieWatched == null)
            {
                return NotFound();
            }
            return View(movieWatched);
        }

        // POST: MovieWatched/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WatcherId,Title")] MovieWatched movieWatched)
        {
            if (id != movieWatched.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieWatched);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieWatchedExists(movieWatched.Id))
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
            return View(movieWatched);
        }

        // GET: MovieWatched/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieWatched = await _context.WatchedMovies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieWatched == null)
            {
                return NotFound();
            }

            return View(movieWatched);
        }

        // POST: MovieWatched/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieWatched = await _context.WatchedMovies.FindAsync(id);
            _context.WatchedMovies.Remove(movieWatched);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieWatchedExists(int id)
        {
            return _context.WatchedMovies.Any(e => e.Id == id);
        }
    }
}
