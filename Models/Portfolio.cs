using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public string AppUserId { get; set; }

        public int RecipeId { get; set; }
        public AppUser AppUser { get; set; }

        public Recipes Recipe { get; set; }
    }
}