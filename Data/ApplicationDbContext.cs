using L8HandsOn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace L8HandsOn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<MovieWatched>? WatchedMovies {get; set;}
    }
}