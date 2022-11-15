using Microsoft.EntityFrameworkCore;
using Practise.DAL.Entities;

namespace Practise.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
