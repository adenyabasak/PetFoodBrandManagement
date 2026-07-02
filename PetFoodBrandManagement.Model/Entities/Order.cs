using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFoodBrandManagement.Model.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string? OrderStatus { get; set; }

        public string? UserName { get; set; }
        public int? UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}