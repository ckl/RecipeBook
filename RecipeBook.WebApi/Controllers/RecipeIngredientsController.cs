using Microsoft.AspNetCore.Http;
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IEnumerable<string> Get()
		{
			//return new string[] { "value1", "value2" };
			throw new NotImplementedException();
		}

		// GET api/<RecipeIngredientsController>/5
		[HttpGet("{recipeID}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<RecipeIngredientDto>>> Get(int recipeID)
		{
			IEnumerable<RecipeIngredientDto> rtrn = await RecipeIngredientsDAL.Get(recipeID);
			return Ok(rtrn);
		}

		// POST api/<RecipeIngredientsController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]	// TODO: consider removing this
		public async Task<IActionResult> Post(IEnumerable<RecipeIngredient> ingredients)
		{
			// TODO: fix this
			if (ingredients == null || !ingredients.Any())
			{
				return BadRequest("No ingredients");
			}

			int recipeId = ingredients.FirstOrDefault().RecipeID;

			if (ingredients.Any(x => x.RecipeID <= 0 || x.IngredientID <= 0))
			{
				throw new ArgumentException("recipeId cannot be 0");
			}

			// TODO: look into ModelState
			//if (!ModelState.IsValid)
			//{
			//	return BadReq
			//}
			await RecipeIngredientsDAL.Upsert(recipeId, ingredients);
			return NoContent();
		}

		// PUT api/<RecipeIngredientsController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Put(int id, [FromBody] string value)
		{
			throw new NotImplementedException();
		}

		// DELETE api/<RecipeIngredientsController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
