using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;
using QRCoder;

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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult Karekod()
        {
            string hedefWebsitesi = " https://listelist.com/hayvanlar-hakkinda-ilginc-bilgiler/#google_vignette";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(hedefWebsitesi, QRCodeGenerator.ECCLevel.Q))
                {
                    using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                    {
                        byte[] qrCodeBytes = qrCode.GetGraphic(20);
                        string base64Gorsel = Convert.ToBase64String(qrCodeBytes);

                        ViewBag.KareKodGorseli = $"data:image/png;base64,{base64Gorsel}";
                    }
                }
            }

            return View();
        }
    }
}