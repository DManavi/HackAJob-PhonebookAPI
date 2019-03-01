
using System.ComponentModel.DataAnnotations;

namespace API.DTO.Auth
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}