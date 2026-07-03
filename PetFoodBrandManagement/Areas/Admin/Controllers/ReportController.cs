using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetFoodBrandManagement.Data.Abstract;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public ReportController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
            string mesaj = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - Admin rapor sayfası açıldı.";
            System.IO.File.AppendAllText(logPath, mesaj + Environment.NewLine);

            var reportData = _memoryCache.GetOrCreate("AdminReportCache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var brands = _unitOfWork.Brand.GetAll();
                var categories = _unitOfWork.Category.GetAll();
                var products = _unitOfWork.Product.GetAll();
                var orders = _unitOfWork.Order.GetAll();
                var reviews = _unitOfWork.Review.GetAll();

                return new
                {
                    BrandCount = brands.Count(),
                    CategoryCount = categories.Count(),
                    ProductCount = products.Count(),
                    OrderCount = orders.Count(),
                    ReviewCount = reviews.Count(),
                    ActiveProduct = products.Count(x => x.Status),
                    TotalStock = products.Sum(x => x.Stock),
                    TotalProductValue = products.Sum(x => x.Stock * x.Price),
                    AveragePrice = products.Any()
                        ? products.Average(x => x.Price).ToString("0.00")
                        : "0"
                };
            });

            ViewBag.BrandCount = reportData.BrandCount;
            ViewBag.CategoryCount = reportData.CategoryCount;
            ViewBag.ProductCount = reportData.ProductCount;
            ViewBag.OrderCount = reportData.OrderCount;
            ViewBag.ReviewCount = reportData.ReviewCount;
            ViewBag.ActiveProduct = reportData.ActiveProduct;
            ViewBag.TotalStock = reportData.TotalStock;
            ViewBag.TotalProductValue = reportData.TotalProductValue;
            ViewBag.AveragePrice = reportData.AveragePrice;

            return View();
        }

        public IActionResult ExportExcel()
        {
            var products = _unitOfWork.Product.GetAll();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ürün Raporu");

            worksheet.Cell(1, 1).Value = "Ürün Adı";
            worksheet.Cell(1, 2).Value = "Fiyat";
            worksheet.Cell(1, 3).Value = "Stok";
            worksheet.Cell(1, 4).Value = "Durum";

            int row = 2;

            foreach (var item in products)
            {
                worksheet.Cell(row, 1).Value = item.ProductName;
                worksheet.Cell(row, 2).Value = item.Price;
                worksheet.Cell(row, 3).Value = item.Stock;
                worksheet.Cell(row, 4).Value = item.Status ? "Aktif" : "Pasif";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "UrunRaporu.xlsx"
            );
        }

        public IActionResult ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var products = _unitOfWork.Product.GetAll().ToList();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("PetFood Ürün Raporu")
                        .SemiBold()
                        .FontSize(22)
                        .FontColor(Colors.Purple.Medium);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Ürün Adı").SemiBold();
                            header.Cell().Text("Fiyat").SemiBold();
                            header.Cell().Text("Stok").SemiBold();
                            header.Cell().Text("Durum").SemiBold();
                        });

                        foreach (var item in products)
                        {
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.Price.ToString("N2") + " TL");
                            table.Cell().Text(item.Stock.ToString());
                            table.Cell().Text(item.Status ? "Aktif" : "Pasif");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("PetFood Brand Management System");
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", "UrunRaporu.pdf");
        }
    }
}