using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROGRA_PARCIAL.Data;
using PROGRA_PARCIAL.Models;
using PROGRA_PARCIAL.Services;
using PROGRA_PARCIAL.ViewModel;
using System.Threading.Tasks;

namespace PROGRA_PARCIAL.Controllers
{
    public class RemesaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyConversionService _currencyService; // El servicio de conversión de moneda
        private readonly ILogger<RemesaController> _logger;

        public RemesaController(ApplicationDbContext context, ICurrencyConversionService currencyService, ILogger<RemesaController> logger)
        {
            _context = context;
            _currencyService = currencyService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Listado()
        {
            var remesas = await _context.DataRemesas.ToListAsync(); // Asegúrate de que sea asincrónico
            return View(remesas);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Remesa remesa)
        {
            if (ModelState.IsValid)
            {
                // Obtener la tasa de cambio
                remesa.TasaCambio = await _currencyService.GetExchangeRateAsync(remesa.TipoMoneda, remesa.TipoMoneda == "USD" ? "BTC" : "USD");

                // Establecer la fecha de envío
                remesa.FechaEnvio = DateTime.Now; // Asignar la fecha actual como fecha de envío
                remesa.Estado = "Pendiente"; // Establecer el estado inicial

                // Agregar la remesa a la base de datos
                await _context.DataRemesas.AddAsync(remesa);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Remesa registrada exitosamente. ID: {remesa.Id}");
                return RedirectToAction(nameof(Listado));
            }

            return View("Index", remesa);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
