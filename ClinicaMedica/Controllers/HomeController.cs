using ClinicaMedica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClinicaMedica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClinicaMedicaContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ClinicaMedicaContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Mantemos APENAS o Index assíncrono que alimenta os cards da Home
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalPacientes = await _context.Paciente.CountAsync();
            ViewBag.TotalConsultas = await _context.Consulta.CountAsync();

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