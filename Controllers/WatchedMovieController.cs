using L8HandsOn.Data;
using L8HandsOn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace L8HandsOn.Controllers
{
    public class WatchMovieController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        public WatchMovieController(ApplicationDbContext context, UserManager<User> userMngr)
        {
            _context = context;
            userManager = userMngr;
        }


        [Authorize]
        public async Task<IActionResult> Watch(Movie movie)
        {
            User applicationUser = await userManager.GetUserAsync(User);
            string userEmail = applicationUser.Email;

            MovieWatched movieWatched = new MovieWatched() { Email = userEmail, Title = movie.Title };
            _context.Add(movieWatched);
            await _context.SaveChangesAsync();
            return View(movieWatched);
        }
    }
}
