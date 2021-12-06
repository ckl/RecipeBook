using Microsoft.EntityFrameworkCore;
using RecipeBook.WebApi.Models;

namespace RecipeBook.WebApi
{
	public class MyDbContext : DbContext
	{
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// TODO: move somewhere else
			var path = "Assets\\Database\\RecipeBookDB.sqlite";
			optionsBuilder.UseSqlite($"Data Source={path};");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
			modelBuilder.Entity<Recipe>().ToTable("Recipes");
			modelBuilder.Entity<RecipeIngredient>().ToTable("RecipeIngredients");
		}
	}
}
