using Microsoft.AspNetCore.Http;
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<Recipe>>> Get()
		{
			return Ok(await RecipeDAL.Get());
		}

		// GET api/<ValuesController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(int id)
		{
			Recipe recipe = await RecipeDAL.Get(id);
			if (recipe is null)
			{
				return NotFound();
			}

			return Ok(recipe);
		}

		// POST api/<ValuesController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Recipe>> Post(Recipe recipe)
		{
			Recipe newRecipe = await RecipeDAL.Insert(recipe);
			return CreatedAtAction(nameof(Post), newRecipe.RecipeID, newRecipe);
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Put(int id, Recipe recipe)
		{
			if (id != recipe.RecipeID)
			{
				return BadRequest();
			}

			await RecipeDAL.Put(recipe);

			return NoContent();
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task Delete(int id)
		{
			await RecipeDAL.Delete(id);
		}
	}
}
