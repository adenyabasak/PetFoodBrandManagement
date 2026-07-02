using Microsoft.EntityFrameworkCore;
using PetFoodBrandManagement.Data.Context;

using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;

namespace PetFoodBrandManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public OrderController(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(x => x.Product)
                .ToList();

            return View(orders);
        }

        public IActionResult Delete(int id)
        {
            var order = _unitOfWork.Order.GetById(id);

            if (order != null)
            {
                _unitOfWork.Order.Delete(order);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}