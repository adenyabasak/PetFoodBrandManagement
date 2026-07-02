using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category, IFormFile? imageFile)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return View(category);
            }

            if (imageFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                category.ImageUrl = "/images/categories/" + fileName;
            }

            category.Status = true;

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _unitOfWork.Category.GetById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                category.ImageUrl = "/images/categories/" + fileName;
            }

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.GetById(id);

            if (category != null)
            {
                _unitOfWork.Category.Delete(category);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}