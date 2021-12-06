using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class Ingredient
	{
		[Key]
		public int IngredientID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedOnUTC { get; set; }
		public DateTime? UpdatedOnUTC { get; set; }
	}
}
