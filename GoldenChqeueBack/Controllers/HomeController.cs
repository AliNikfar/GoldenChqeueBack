using GoldenChequeBack.Service.Contract;
using GoldenChqeueBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoldenChqeueBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBankRepository _bank;

        public HomeController(IBankRepository bank)
        {
            _bank = bank;
        }

        public IActionResult Index()
        {
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