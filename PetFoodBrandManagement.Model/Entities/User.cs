using System.ComponentModel.DataAnnotations;

namespace PetFoodBrandManagement.Model.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Email { get; set; }

        public string Role { get; set; } = "User";

        public bool Status { get; set; }
    }
}