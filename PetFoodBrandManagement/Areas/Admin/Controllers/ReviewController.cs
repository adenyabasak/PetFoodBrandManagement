using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Data.Context;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public ReviewController(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            var reviews = _context.Reviews
                .Include(x => x.Product)
                .ToList();

            return View(reviews);
        }

        public IActionResult Delete(int id)
        {
            var review = _unitOfWork.Review.GetById(id);

            if (review != null)
            {
                _unitOfWork.Review.Delete(review);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}