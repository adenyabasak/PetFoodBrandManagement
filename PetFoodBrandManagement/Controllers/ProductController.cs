using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFoodBrandManagement.Data.Context;

namespace PetFoodBrandManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Status)
                .ToList();

            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}