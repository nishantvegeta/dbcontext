using System;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class FirstRunDbContext : DbContext
    {
        public FirstRunDbContext(DbContextOptions<FirstRunDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.ProductCategory> ProductCategories { get; set; }
    }
}