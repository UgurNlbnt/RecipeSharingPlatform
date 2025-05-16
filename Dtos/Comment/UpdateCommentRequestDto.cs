using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Başlık en az 5 karakter olmalıdır.")]
        [MaxLength(100, ErrorMessage = "Başlık en fazla 100 karakter olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "İçerik yazısı en az 2 karakter olmalıdır.")]
        [MaxLength(1000, ErrorMessage = "İçerik yazısı en fazla 100 karakter olmalıdır.")]
        public string Content { get; set; } = string.Empty;
    }
}