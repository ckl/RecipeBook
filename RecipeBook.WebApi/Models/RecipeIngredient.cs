using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class RecipeIngredient 
	{
		[Key]
		public int RecipeIngredientID { get; set; }
		public int RecipeID { get; set; }
		public int IngredientID { get; set; }
		public string Quantity { get; set; }
		public string Notes { get; set; }
	}
}
