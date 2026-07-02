using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var brands = _unitOfWork.Brand.GetAll();
            var categories = _unitOfWork.Category.GetAll();
            var products = _unitOfWork.Product.GetAll();
            var orders = _unitOfWork.Order.GetAll();
            var reviews = _unitOfWork.Review.GetAll();

            ViewBag.BrandCount = brands.Count();
            ViewBag.CategoryCount = categories.Count();
            ViewBag.ProductCount = products.Count();
            ViewBag.OrderCount = orders.Count();
            ViewBag.ReviewCount = reviews.Count();

            ViewBag.ActiveProduct = products.Count(x => x.Status);

            ViewBag.TotalStock = products.Sum(x => x.Stock);

            ViewBag.TotalProductValue = products.Sum(x => x.Stock * x.Price);

            ViewBag.AveragePrice = products.Any()
                ? products.Average(x => x.Price).ToString("0.00")
                : "0";

            return View();
        }
    }
}