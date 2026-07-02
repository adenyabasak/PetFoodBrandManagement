using System.ComponentModel.DataAnnotations;

namespace PetFoodBrandManagement.Model.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        public string BrandName { get; set; }

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}