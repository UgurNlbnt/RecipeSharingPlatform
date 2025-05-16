using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Dtos.Recipe
{
    public class CreateRecipeRequestDto
    {
        [Required(ErrorMessage = "Tarif URL bilgisi gereklidir.")]
        [MinLength(5, ErrorMessage = "Tarif URL bilgisi en az 5 karakter olmalıdır.")]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tarif adı gereklidir.")]
        [MinLength(5, ErrorMessage = "Tarif adı en az 5 karakter olmalıdır.")]
        public string RecipeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hazırlık süresi gereklidir.")]
        [Range(1, 500, ErrorMessage = "Hazırlık süresi 1 ile 500 dakika arasında olmalıdır.")]
        public int PreparationTime { get; set; }

        [Required(ErrorMessage = "Pişirme süresi gereklidir.")]
        public int CookTime { get; set; }

        [Required(ErrorMessage = "Kategori bilgisi gereklidir.")]
        [MinLength(3, ErrorMessage = "Kategori adı en az 3 karakter olmalıdır.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Porsiyon bilgisi gereklidir.")]
        [Range(1, 100, ErrorMessage = "Porsiyon miktarı 1 ile 100 arasında olmalıdır.")]
        public int ServingSize { get; set; }

        [Required(ErrorMessage = "İçerik bilgisi gereklidir.")]
        [MinLength(5, ErrorMessage = "İçerik bilgisi en az 5 karakter olmalıdır.")]
        public string Ingredients { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yapılış talimatı gereklidir.")]
        [MinLength(5, ErrorMessage = "Yapılış talimatı en az 5 karakter olmalıdır.")]
        public string Instructions { get; set; } = string.Empty;
    }

}
