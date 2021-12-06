using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
	public class IngredientController : ControllerBase
	{
		// GET: api/<IngredientController>
		[HttpGet]
		public IEnumerable<Ingredient> Get()
		{
			//DatabaseManager.Insert();
			return IngredientDAL.Get();
		}

		// GET api/<IngredientController>/5
		[HttpGet("{id}")]
		public async Task<Ingredient> Get(int id)
		{
			return await IngredientDAL.Get(id);
		}

		// POST api/<IngredientController>
		[HttpPost]
		public async Task<ActionResult<Ingredient>> Post(Ingredient ingredient)
		{
			Ingredient newIngredient = IngredientDAL.Post(ingredient);
			return CreatedAtAction(nameof(Get), newIngredient.IngredientID, newIngredient);
		}

		// PUT api/<IngredientController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Ingredient ingredient)
		{
			if (id != ingredient.IngredientID)
			{
				return BadRequest();
			}

			try
			{
				IngredientDAL.Put(ingredient);
			}
			catch (DbUpdateConcurrencyException ex)
			{

			}
			catch (Exception ex)
			{

			}

			return NoContent();
		}

		// DELETE api/<IngredientController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			//Ingredient ingredient = DatabaseManager.Get(id);
			//if (ingredient == null)
			//{
			//	return NotFound();
			//}

			IngredientDAL.Delete(id);
			
			return NoContent();
		}
	}
}
