using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var news = _unitOfWork.News
                .GetAll()
                .Where(x => x.Status)
                .OrderByDescending(x => x.NewsDate)
                .Take(4)
                .ToList();

            return View(news);
        }
    }
}