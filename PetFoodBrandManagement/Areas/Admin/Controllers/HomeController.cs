using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
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

            ViewBag.Brand = brands.Count();
            ViewBag.Category = categories.Count();
            ViewBag.Product = products.Count();
            ViewBag.Order = orders.Count();
            ViewBag.Review = reviews.Count();

            ViewBag.Stock = products.Sum(x => x.Stock);

            ViewBag.AveragePrice = products.Any()
                ? products.Average(x => x.Price).ToString("0.00")
                : "0";

            return View();
        }
    }
}