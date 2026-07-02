using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var news = _unitOfWork.News.GetAll();
            return View(news);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(News news)
        {
            news.NewsDate = DateTime.Now;
            news.Status = true;

            _unitOfWork.News.Add(news);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var news = _unitOfWork.News.GetById(id);
            return View(news);
        }

        [HttpPost]
        public IActionResult Update(News news)
        {
            _unitOfWork.News.Update(news);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var news = _unitOfWork.News.GetById(id);

            if (news != null)
            {
                _unitOfWork.News.Delete(news);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}