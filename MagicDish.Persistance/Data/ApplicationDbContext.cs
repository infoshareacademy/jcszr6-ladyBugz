using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MagicDish.Core.Models;

namespace MagicDish.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> AvailableProducts { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
    }
}