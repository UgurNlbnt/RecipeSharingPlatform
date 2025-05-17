using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Dtos.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Kullanıcı adı girilmesi zorunludur.")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}