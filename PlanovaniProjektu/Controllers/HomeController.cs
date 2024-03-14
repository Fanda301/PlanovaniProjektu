using Microsoft.AspNetCore.Mvc;
using PlanovaniProjektu.Models;
using System.Diagnostics;

namespace PlanovaniProjektu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DbProjektyContext _conn = new DbProjektyContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string b = CheckUser.ChechAutorizationToken(HttpContext.Session.GetString("AutorizacniToken"));


            if (b == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Autorizovano = b;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}