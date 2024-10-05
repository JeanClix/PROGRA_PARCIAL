using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROGRA_PARCIAL.Services;
using PROGRA_PARCIAL.Data;
using PROGRA_PARCIAL.Models;

namespace PROGRA_PARCIAL.Controllers
{
    public class RemesaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyConversionService _currencyService;

        public RemesaController(ApplicationDbContext context, ICurrencyConversionService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            var remesas = _context.DataRemesas.ToList();
            return View(remesas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Remesa remesa)
        {
            if (ModelState.IsValid)
            {
                remesa.TasaCambio = await _currencyService.GetExchangeRateAsync(remesa.TipoMoneda, remesa.TipoMoneda == "USD" ? "BTC" : "USD");

                // Corregimos el cálculo del MontoFinal
                if (remesa.TipoMoneda == "USD")
                {
                    // USD a BTC: multiplicamos por la tasa (que será un número pequeño)
                    remesa.MontoFinal = remesa.MontoEnviado / remesa.TasaCambio;
                }
                else // BTC
                {
                    // BTC a USD: multiplicamos por la tasa (que será un número grande)
                    remesa.MontoFinal = remesa.MontoEnviado * remesa.TasaCambio;
                }

                _context.DataRemesas.Add(remesa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Listado));
            }
            return View("Index", remesa);
        }
    }
}