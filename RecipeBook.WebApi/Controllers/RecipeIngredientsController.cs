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
			return await RecipeIngredientsDAL.Get(recipeID);
		}

		// POST api/<RecipeIngredientsController>
		[HttpPost]
		public void Post(IEnumerable<RecipeIngredient> ingredients)
		{
			// TODO: fix this
			if (ingredients?.Count() > 0)
			{
				RecipeIngredientsDAL.Upsert(ingredients.ToList()[0].RecipeID, ingredients);
			}
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
