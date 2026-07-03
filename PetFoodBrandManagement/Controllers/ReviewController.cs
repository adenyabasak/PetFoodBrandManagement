using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFoodBrandManagement.Data.Context;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            ViewBag.Product = product;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Review review)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var fullName = HttpContext.Session.GetString("FullName");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            review.ReviewDate = DateTime.Now;
            review.Status = true;
            review.UserId = userId;
            review.UserName = fullName ?? "Kullanıcı";

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("MyReviews");
        }

        public IActionResult MyReviews()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var reviews = _context.Reviews
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.ReviewDate)
                .ToList();

            return View(reviews);
        }
    }
}