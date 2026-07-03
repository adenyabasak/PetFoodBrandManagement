using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFoodBrandManagement.Model.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime ReviewDate { get; set; }

        public bool Status { get; set; }

        public int? UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}