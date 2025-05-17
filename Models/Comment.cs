using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarifPaylasim.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? RecipeId { get; set; }

        [JsonIgnore]
        public Recipes? Recipe { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } = new AppUser();
    }
}