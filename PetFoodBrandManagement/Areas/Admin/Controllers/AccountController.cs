using Microsoft.AspNetCore.Mvc;
using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string role)
        {
            user.Role = role;
            user.Status = true;

            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password, string role)
        {
            var user = _unitOfWork.User.GetAll()
                .FirstOrDefault(x =>
                    x.UserName == userName &&
                    x.Password == password &&
                    x.Role == role &&
                    x.Status);

            if (user == null)
            {
                ViewBag.Error = "Kullanıcı adı, şifre veya giriş türü hatalı.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("FullName", user.FullName);
            HttpContext.Session.SetString("Role", user.Role);

            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
            string mesaj = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} | LOGIN | {user.FullName} sisteme giriş yaptı. Rol: {user.Role}";
            System.IO.File.AppendAllText(logPath, mesaj + Environment.NewLine);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            string fullName = HttpContext.Session.GetString("FullName") ?? "Bilinmeyen Kullanıcı";

            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
            string mesaj = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} | LOGOUT | {fullName} sistemden çıkış yaptı.";
            System.IO.File.AppendAllText(logPath, mesaj + Environment.NewLine);

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}