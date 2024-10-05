using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROGRA_PARCIAL.Data;
using PROGRA_PARCIAL.Models;
using PROGRA_PARCIAL.Services;

namespace PROGRA_PARCIAL.Controllers
{
    [Route("[controller]")]
    public class RemesasController : Controller
    {
        private readonly ILogger<RemesasController> _logger;
        private readonly ApplicationDbContext _context; // Inyectamos el contexto de la base de datos
        private readonly CurrencyConversionService _conversionService; // Inyectamos el servicio de conversión

        public RemesasController(ILogger<RemesasController> logger, ApplicationDbContext context, CurrencyConversionService conversionService)
        {
            _logger = logger;
            _context = context; // Inyectamos el contexto
            _conversionService = conversionService; // Inyectamos el servicio de conversión
        }

        // Acción para mostrar el listado de Remesas
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var Remesas = await _context.DataRemesas.ToListAsync();
            return View(Remesas); // Pasamos la lista de Remesas a la vista
        }

        // Acción para mostrar el formulario de registro
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // Acción para registrar una nueva transacción
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Remesa Remesas)
        {
            if (ModelState.IsValid)
            {
                // Si la transacción es en USD, calculamos el monto final usando la tasa de cambio
                if (Remesas.TipoMoneda == "USD")
                {
                     // Convertir de USD a BTC
                    try
                    {
                        Remesas.MontoFinal = await _conversionService.ConvertUsdToBtc(Remesas.MontoEnviado);
                        Remesas.TasaCambio = await _conversionService.GetBtcToUsdRateAsync(); // Guarda la tasa actual
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores al obtener la tasa de conversión
                        ModelState.AddModelError("", "Error al obtener la tasa de conversión: " + ex.Message);
                        return View(Remesas);
                    }
                }
                // Si la transacción es en BTC, utilizamos ConversionService para obtener la tasa de conversión
                else if (Remesas.TipoMoneda == "BTC")
                {
                    try
                    {
                        var tasaBtc = await _conversionService.GetBtcToUsdRateAsync();
                        // Establecer la tasa de cambio como el precio del dólar actual
                        Remesas.TipoMoneda = 1/tasaBtc; // Esto convierte la tasa de BTC a USD a USD a BTC
                        Remesas.MontoFinal = Remesas.MontoEnviado * tasaBtc; // Calculamos el monto final en BTC
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores al obtener la tasa de conversión
                        ModelState.AddModelError("", "Error al obtener la tasa de conversión BTC: " + ex.Message);
                        return View(Remesas);
                    }
                }

                // Guardar la transacción en la base de datos
                _context.Add(Remesas);
                await _context.SaveChangesAsync();

                // Redirigir al listado después de guardar
                return RedirectToAction(nameof(Index));
            }

            // En caso de que el modelo no sea válido, regresar la vista con el modelo
            return View(Remesas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}