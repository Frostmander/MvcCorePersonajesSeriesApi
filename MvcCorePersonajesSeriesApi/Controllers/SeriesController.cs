using Microsoft.AspNetCore.Mvc;
using MvcCorePersonajesSeriesApi.Models;
using MvcCorePersonajesSeriesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePersonajesSeriesApi.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceSeries service;
        public SeriesController(ServiceSeries service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        public async Task<IActionResult> DetailsSerie(int idserie)
        {
            return View(await this.service.FindSerieAsync(idserie));
        }

        public async Task<IActionResult> DetailsPersonaje(int idpersonaje)
        {
            return View(await this.service.FindpersonajeAsync(idpersonaje));
        }

        public async Task<IActionResult> PersonajesSerie(int idserie)
        {
            return View(await this.service.PersonajesSeriesAsync(idserie));
        }

        public async Task<IActionResult> CambiarPersonajeSerie()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewBag.personajes=personajes;
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPersonajeSerie(int idpersonaje, int idserie)
        {
            await this.service.CambiarPersonajeSerie(idpersonaje, idserie);
            return RedirectToAction("Index");
        }
    }
}
