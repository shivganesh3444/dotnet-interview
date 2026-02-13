using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "User";
    }
}