using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipi_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, Name = "Soep" },
                new Category() { CategoryId = 2, Name = "Dessert" },
                new Category() { CategoryId = 3, Name = "Hoofdgerect" },
                new Category() { CategoryId = 4, Name = "Voorgerecht" },
                new Category() { CategoryId = 5, Name = "Vegitarisch" }
            );
        }


        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
