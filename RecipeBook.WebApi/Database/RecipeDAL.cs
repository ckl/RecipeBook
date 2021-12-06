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
	public static class RecipeDAL
	{
		public static Recipe Insert(Recipe recipe)
		{
			using (var context = new MyDbContext())
			{
				recipe.RecipeID = 0;
				context.Recipes.Add(recipe);
				context.SaveChanges();

				return recipe;
			}
		}

		public static IEnumerable<Recipe> Get()
		{
			using (var context = new MyDbContext())
			{
				List<Recipe> recipes = new List<Recipe>();
				foreach (var i in context.Recipes)
				{
					recipes.Add(i);
				}
				return recipes;
			}
		}

		public static Task<Recipe> Get(int id)
		{
			using (var context = new MyDbContext())
			{
				return context.Recipes.FirstOrDefaultAsync(x => x.RecipeID == id);
			}
		}

		public static void Put(Recipe recipe)
		{
			using (var context = new MyDbContext())
			{
				context.Entry(recipe).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public static void Delete(int recipeId)
		{
			using (var context = new MyDbContext())
			{
				var ingredients = context.RecipeIngredients.Where(x => x.RecipeID == recipeId);

				foreach (var ingredient in ingredients)
				{
					if (ingredient == null)
					{
						continue;
					}

					context.RecipeIngredients.Remove(ingredient);
				}

				var recipe = context.Recipes.Find(recipeId);

				if (recipe != null)
				{
					context.Recipes.Remove(recipe);
				}

				context.SaveChanges();
			}
		}
	}
}
