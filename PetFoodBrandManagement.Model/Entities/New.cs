using System.ComponentModel.DataAnnotations;

namespace PetFoodBrandManagement.Model.Entities
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime NewsDate { get; set; }

        public bool Status { get; set; }
    }
}