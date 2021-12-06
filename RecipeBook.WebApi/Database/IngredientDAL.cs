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
		public static void Insert()
		{
			using (var context = new MyDbContext())
			{
				var ingredient = new Ingredient
				{
					Name = "Lettuce",
					Notes = "cruncy water"
				};

				context.Ingredients.Add(ingredient);
				context.SaveChanges();

				foreach (var i in context.Ingredients)
				{
					var x = i;
				}
			}
		}

		public static IEnumerable<Ingredient> Get()
		{
			using (var context = new MyDbContext())
			{
				List<Ingredient> ingredients = new List<Ingredient>();
				foreach (var i in context.Ingredients)
				{
					ingredients.Add(i);
				}
				return ingredients;
			}
		}

		public static Task<Ingredient> Get(int id)
		{
			using (var context = new MyDbContext())
			{
				return context.Ingredients.FirstOrDefaultAsync(x => x.IngredientID == id);
			}
		}

		public static Ingredient Post(Ingredient ingredient)
		{
			using (var context = new MyDbContext())
			{
				context.Ingredients.Add(ingredient);
				context.SaveChanges();

				return ingredient;
			}
		}

		public static void Put(Ingredient ingredient)
		{
			using (var context = new MyDbContext())
			{
				context.Entry(ingredient).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (var context = new MyDbContext())
			{
				var ingredient = context.Ingredients.Find(id);

				if (ingredient == null)
				{
					return;
				}

				context.Ingredients.Remove(ingredient);
				context.SaveChanges();
			}
		}
	}
}
