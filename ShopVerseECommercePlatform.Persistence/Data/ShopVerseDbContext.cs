using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Persistence.Data
{
    public class ShopVerseDbContext : DbContext
    {
        public ShopVerseDbContext(DbContextOptions<ShopVerseDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<>().HasData()
        }
    }
}
