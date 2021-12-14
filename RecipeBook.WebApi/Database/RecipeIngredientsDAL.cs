using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBook.WebApi.Models;

namespace RecipeBook.WebApi.Database
{
	// TODO: make this a singleton
	public static class RecipeIngredientsDAL
	{
		public async static Task Upsert(int recipeId, IEnumerable<RecipeIngredient> ingredients)
		{
			using (var context = new MyDbContext())
			{
				// TODO: rework this
				var existing = context.RecipeIngredients.Where(x => x.RecipeId == recipeId);
				foreach (var i in existing) {
					context.Entry(i).State = EntityState.Deleted;
				}
				context.SaveChanges();

				foreach (var i in ingredients)
				{
					context.RecipeIngredients.Add(new RecipeIngredient
					{
						IngredientId = i.IngredientId,
						RecipeId = recipeId,
						Quantity = i.Quantity,
						Notes = i.Notes
					});
				}
				await context.SaveChangesAsync();
			}
		}

		public async static Task<IEnumerable<RecipeIngredientDto>> Get(int recipeId)
		{
			using (var context = new MyDbContext())
			{
				IQueryable<RecipeIngredientDto> query =  
					(from i in context.Ingredients
					join ri in context.RecipeIngredients
					on i.IngredientId equals ri.IngredientId
					where ri.RecipeId == recipeId
					select new RecipeIngredientDto
					{
						IngredientId = i.IngredientId,
						Name = i.Name,
						Notes = ri.Notes,
						RecipeId = recipeId,
						Quantity = ri.Quantity
					});

				return await query.ToListAsync();
			}
		}
	}
}
