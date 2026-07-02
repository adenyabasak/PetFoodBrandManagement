using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Brands = new SelectList(_unitOfWork.Brand.GetAll(), "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

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

                product.ImageUrl = "/images/products/" + fileName;
            }

            product.Status = true;

            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _unitOfWork.Product.GetById(id);

            ViewBag.Brands = new SelectList(_unitOfWork.Brand.GetAll(), "BrandId", "BrandName", product.BrandId);
            ViewBag.Categories = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "CategoryName", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

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

                product.ImageUrl = "/images/products/" + fileName;
            }

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetById(id);

            if (product != null)
            {
                _unitOfWork.Product.Delete(product);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}