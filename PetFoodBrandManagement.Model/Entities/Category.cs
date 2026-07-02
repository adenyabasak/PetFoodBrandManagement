using System.ComponentModel.DataAnnotations;

namespace PetFoodBrandManagement.Model.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}