using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Models
{
    [Table("Recipes")]
    public class Recipes
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string RecipeName { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int CookTime { get; set; }
        public string Category { get; set; } = string.Empty;
        public int ServingSize { get; set; }
        public string Ingredients { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}