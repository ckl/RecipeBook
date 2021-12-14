using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBook.WebApi.Models;

namespace RecipeBook.WebApi.Database
{
	// TODO: make this a singleton
	// TODO: somehow make this and all other calls await and use .SaveChangesAsync()
	public static class IngredientDAL
	{
		public async static Task<IEnumerable<Ingredient>> Get()
		{
			using (var context = new MyDbContext())
			{
				return await context.Ingredients.ToListAsync();
			}
		}

		public async static Task<Ingredient> Get(int id)
		{
			using (var context = new MyDbContext())
			{
				return await context.Ingredients.FirstOrDefaultAsync(x => x.IngredientId == id);
			}
		}

		public async static Task<Ingredient> Post(Ingredient ingredient)
		{
			using (var context = new MyDbContext())
			{
				ingredient.CreatedOnUtc = DateTime.UtcNow;
				ingredient.UpdatedOnUtc = DateTime.UtcNow;
				context.Ingredients.Add(ingredient);
				await context.SaveChangesAsync();

				return ingredient;
			}
		}

		public async static Task Put(Ingredient ingredient)
		{
			using (var context = new MyDbContext())
			{
				ingredient.UpdatedOnUtc = DateTime.UtcNow;
				context.Entry(ingredient).State = EntityState.Modified;
				await context.SaveChangesAsync();
			}
		}

		public async static Task Delete(int id)
		{
			using (var context = new MyDbContext())
			{
				var ingredient = context.Ingredients.Find(id);

				if (ingredient == null)
				{
					return;
				}

				context.Ingredients.Remove(ingredient);
				await context.SaveChangesAsync();
			}
		}
	}
}
