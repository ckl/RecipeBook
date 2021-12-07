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
	public static class RecipeIngredientsDAL
	{
		public static RecipeIngredient Insert(RecipeIngredient ingredient)
		{
			using (var context = new MyDbContext())
			{
				ingredient.RecipeID = 0;
				context.RecipeIngredients.Add(ingredient);
				context.SaveChanges();

				return ingredient;
			}
		}

		//public static IEnumerable<RecipeIngredient> Get()
		//{
		//	using (var context = new MyDbContext())
		//	{
		//		List<RecipeIngredient> recipes = new List<RecipeIngredient>();
		//		foreach (var i in context.RecipeIngredients)
		//		{
		//			recipes.Add(i);
		//		}
		//		return recipes;
		//	}
		//}

		public static void Upsert(int recipeID, IEnumerable<RecipeIngredient> ingredients)
		{
			using (var context = new MyDbContext())
			{
				// TODO: rework this
				var existing = context.RecipeIngredients.Where(x => x.RecipeID == recipeID);
				foreach (var i in existing) {
					context.Entry(i).State = EntityState.Deleted;
				}
				context.SaveChanges();

				foreach (var i in ingredients)
				{
					//Insert(new RecipeIngredient
					//{
					//	IngredientID = i.IngredientID,
					//	RecipeID = recipeID,
					//	Quantity = i.Quantity,
					//	Notes = i.Notes
					//});

					context.RecipeIngredients.Add(new RecipeIngredient
					{
						IngredientID = i.IngredientID,
						RecipeID = recipeID,
						Quantity = i.Quantity,
						Notes = i.Notes
					});
					context.SaveChanges();
				}
			}
		}

		public static async Task<IEnumerable<RecipeIngredientDto>> Get(int recipeID)
		{
			using (var context = new MyDbContext())
			{
				var x = (from i in context.Ingredients
						 join ri in context.RecipeIngredients
						 on i.IngredientID equals ri.IngredientID
						 where ri.RecipeID == recipeID
						 select new RecipeIngredientDto
						 {
							 IngredientID = i.IngredientID,
							 Name = i.Name,
							 Notes = i.Notes,
							 RecipeID = recipeID,
							 Quantity = ri.Quantity
						 });

				return await x.ToListAsync();
				//return await context.RecipeIngredients.Where(x => x.RecipeID == recipeID).ToListAsync();
			}
		}

		public static void Put(RecipeIngredient recipe)
		{
			using (var context = new MyDbContext())
			{
				context.Entry(recipe).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (var context = new MyDbContext())
			{
				var ingredient = context.RecipeIngredients.Find(id);

				if (ingredient == null)
				{
					return;
				}

				context.RecipeIngredients.Remove(ingredient);
				context.SaveChanges();
			}
		}
	}
}
