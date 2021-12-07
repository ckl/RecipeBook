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
	public class RecipesController : ControllerBase
	{
		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<Recipe> Get()
		{
			return RecipeDAL.Get();
		}

		// GET api/<ValuesController>/5
		[HttpGet("{id}")]
		public async Task<Recipe> Get(int id)
		{
			return await RecipeDAL.Get(id);
		}

		// POST api/<ValuesController>
		[HttpPost]
		public async Task<ActionResult<Recipe>> Post(Recipe recipe)
		{
			Recipe newRecipe = RecipeDAL.Insert(recipe);
			return CreatedAtAction(nameof(Post), newRecipe.RecipeID, newRecipe);
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Recipe recipe)
		{
			if (id != recipe.RecipeID)
			{
				return BadRequest();
			}

			try
			{
				RecipeDAL.Put(recipe);
			}
			catch (DbUpdateConcurrencyException)
			{

			}
			catch (Exception)
			{

			}

			return NoContent();
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			RecipeDAL.Delete(id);
		}
	}
}
