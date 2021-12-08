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

		public async static Task Upsert(int recipeID, IEnumerable<RecipeIngredient> ingredients)
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
					context.RecipeIngredients.Add(new RecipeIngredient
					{
						IngredientID = i.IngredientID,
						RecipeID = recipeID,
						Quantity = i.Quantity,
						Notes = i.Notes
					});
				}
				await context.SaveChangesAsync();
			}
		}

		public async static Task<IEnumerable<RecipeIngredientDto>> Get(int recipeID)
		{
			using (var context = new MyDbContext())
			{
				IQueryable<RecipeIngredientDto> query =  
					(from i in context.Ingredients
					join ri in context.RecipeIngredients
					on i.IngredientID equals ri.IngredientID
					where ri.RecipeID == recipeID
					select new RecipeIngredientDto
					{
						IngredientID = i.IngredientID,
						Name = i.Name,
						Notes = ri.Notes,
						RecipeID = recipeID,
						Quantity = ri.Quantity
					});

				return await query.ToListAsync();
			}
		}
	}
}
