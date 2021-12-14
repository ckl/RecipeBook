using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class Ingredient
	{
		[Key]
		public int IngredientId { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedOnUtc { get; set; }
		public DateTime? UpdatedOnUtc { get; set; }
	}
}
