using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var brands = _unitOfWork.Brand.GetAll();
            return View(brands);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Brand brand, IFormFile? logoFile)
        {
            if (string.IsNullOrWhiteSpace(brand.BrandName))
            {
                ModelState.AddModelError("BrandName", "Marka adı boş geçilemez.");
                return View(brand);
            }

            if (logoFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "brands");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(stream);
                }

                brand.LogoUrl = "/images/brands/" + fileName;
            }

            brand.Status = true;

            _unitOfWork.Brand.Add(brand);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _unitOfWork.Brand.GetById(id);
            return View(brand);
        }

        [HttpPost]
        public IActionResult Update(Brand brand, IFormFile? logoFile)
        {
            if (logoFile != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "brands");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(stream);
                }

                brand.LogoUrl = "/images/brands/" + fileName;
            }

            _unitOfWork.Brand.Update(brand);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var brand = _unitOfWork.Brand.GetById(id);

            if (brand != null)
            {
                _unitOfWork.Brand.Delete(brand);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}