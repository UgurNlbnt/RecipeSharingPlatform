using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Dtos
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string RecipeName { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int CookTime { get; set; }
        public string Category { get; set; } = string.Empty;
        public int ServingSize { get; set; }
    }
}