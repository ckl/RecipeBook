using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class RecipeIngredientDto 
	{
		public int RecipeId { get; set; }
		public int IngredientId { get; set; }
		public string Name { get; set; }
		public string Quantity { get; set; }
		public string Notes { get; set; }
	}
}
