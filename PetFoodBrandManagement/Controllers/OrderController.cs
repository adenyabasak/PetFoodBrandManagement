using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFoodBrandManagement.Data.Context;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            ViewBag.Product = product;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == order.ProductId);

            if (product == null)
                return NotFound();

            order.OrderDate = DateTime.Now;
            order.OrderStatus = "Hazırlanıyor";
            order.TotalPrice = product.Price * order.Quantity;

            order.UserId = HttpContext.Session.GetInt32("UserId");
            order.UserName = HttpContext.Session.GetString("UserName");

            _context.Orders.Add(order);
            _context.SaveChanges();

            string fullName = HttpContext.Session.GetString("FullName") ?? "Bilinmeyen Kullanıcı";
            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
            string mesaj = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} | ORDER | {fullName}, {product.ProductName} ürününden {order.Quantity} adet sipariş oluşturdu.";
            System.IO.File.AppendAllText(logPath, mesaj + Environment.NewLine);

            return RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var orders = _context.Orders
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToList();

            return View(orders);
        }
    }
}