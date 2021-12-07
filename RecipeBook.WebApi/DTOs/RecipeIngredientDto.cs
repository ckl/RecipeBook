using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class RecipeIngredientDto 
	{
		public int RecipeID { get; set; }
		public int IngredientID { get; set; }
		public string Name { get; set; }
		public string Quantity { get; set; }
		public string Notes { get; set; }
	}
}
