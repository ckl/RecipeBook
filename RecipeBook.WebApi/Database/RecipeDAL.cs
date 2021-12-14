using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBook.WebApi.Models;

namespace RecipeBook.WebApi.Database
{
	// TODO: make this a singleton
	public static class RecipeDAL
	{
		public async static Task<Recipe> Insert(Recipe recipe)
		{
			using (var context = new MyDbContext())
			{
				recipe.CreatedOnUtc = DateTime.UtcNow;
				recipe.UpdatedOnUtc = DateTime.UtcNow;

				context.Recipes.Add(recipe);
				await context.SaveChangesAsync();

				return recipe;
			}
		}

		public async static Task<IEnumerable<Recipe>> Get()
		{
			using (var context = new MyDbContext())
			{
				return await context.Recipes.ToListAsync();
			}
		}

		public static Task<Recipe> Get(int id)
		{
			using (var context = new MyDbContext())
			{
				return context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == id);
			}
		}

		public async static Task Put(Recipe recipe)
		{
			using (var context = new MyDbContext())
			{
				recipe.UpdatedOnUtc = DateTime.UtcNow;
				context.Entry(recipe).State = EntityState.Modified;
				await context.SaveChangesAsync();
			}
		}

		public async static Task Delete(int recipeId)
		{
			using (var context = new MyDbContext())
			{
				// /first remove the ingredients
				var ingredients = context.RecipeIngredients.Where(x => x.RecipeId == recipeId);

				foreach (var ingredient in ingredients)
				{
					if (ingredient == null)
					{
						continue;
					}

					context.RecipeIngredients.Remove(ingredient);
				}

				// remove the recipe
				var recipe = context.Recipes.Find(recipeId);

				if (recipe != null)
				{
					context.Recipes.Remove(recipe);
				}

				await context.SaveChangesAsync();
			}
		}
	}
}
