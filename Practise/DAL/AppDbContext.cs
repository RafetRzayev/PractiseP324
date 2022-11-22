using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practise.DAL.Entities;
using System.Data;

namespace Practise.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<WishList> WishList { get; set; }
        public DbSet<WishListProduct> WishListProduct { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
