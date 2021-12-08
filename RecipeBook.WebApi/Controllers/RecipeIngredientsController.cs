using Microsoft.AspNetCore.Mvc;
using RecipeBook.WebApi.Database;
using RecipeBook.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeBook.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeIngredientsController : ControllerBase
	{
		// GET: api/<RecipeIngredientsController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			//return new string[] { "value1", "value2" };
			throw new NotImplementedException();
		}

		// GET api/<RecipeIngredientsController>/5
		[HttpGet("{recipeID}")]
		public async Task<IEnumerable<RecipeIngredientDto>> Get(int recipeID)
		{
			IEnumerable<RecipeIngredientDto> rtrn = await RecipeIngredientsDAL.Get(recipeID);
			return rtrn;
		}

		// POST api/<RecipeIngredientsController>
		[HttpPost]
		public void Post(IEnumerable<RecipeIngredient> ingredients)
		{
			// TODO: fix this
			if (ingredients == null || !ingredients.Any())
			{
				return;
			}

			int recipeId = ingredients.FirstOrDefault().RecipeID;

			if (ingredients.Any(x => x.RecipeID <= 0 || x.IngredientID <= 0))
			{
				throw new ArgumentException("recipeId cannot be 0");
			}

			RecipeIngredientsDAL.Upsert(recipeId, ingredients);
		}

		// PUT api/<RecipeIngredientsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
			throw new NotImplementedException();
		}

		// DELETE api/<RecipeIngredientsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
