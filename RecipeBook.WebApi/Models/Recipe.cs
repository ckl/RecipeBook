﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.WebApi.Models
{
	public class Recipe 
	{
		[Key]
		public int RecipeID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int CookTimeMinutes { get; set; }
		public string Directions { get; set; }
		public string Notes { get; set; }
		public DateTime CreatedOnUTC { get; set; }
		public DateTime UpdatedOnUTC { get; set; }
	}
}
