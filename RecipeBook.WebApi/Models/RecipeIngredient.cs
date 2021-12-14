using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class RecipeIngredient 
	{
		[Key]
		public int RecipeIngredientId { get; set; }
		public int RecipeId { get; set; }
		public int IngredientId { get; set; }
		public string Quantity { get; set; }
		public string Notes { get; set; }
	}
}
