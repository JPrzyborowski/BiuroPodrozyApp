using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiuroPodrozyApp.Data;
using BiuroPodrozyApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BiuroPodrozyApp.Controllers
{
    public class SzukajWycieczkiController : Controller
    {
        private readonly BiuroContext _context;

        public SzukajWycieczkiController(BiuroContext context)
        {
            _context = context;
        }

        // GET: SzukajWycieczki
        public async Task<IActionResult> Index(string nazwa, DateTime? dataOd, DateTime? dataDo, decimal? cena)
        {
            IQueryable<Wycieczka> wycieczki = _context.Wycieczki;

            if (!string.IsNullOrEmpty(nazwa))
            {
                wycieczki = _context.Wycieczki.Where(w => w.Nazwa.Contains(nazwa) || w.Opis.Contains(nazwa));
            }

            if (dataOd.HasValue)
            {
                wycieczki = wycieczki.Where(w => w.DataOd >= dataOd.Value);
            }

            if (dataDo.HasValue)
            {
                wycieczki = wycieczki.Where(w => w.DataOd <= dataDo.Value);
            }

            if (cena.HasValue)
            {
                wycieczki = wycieczki.Where(w => w.Cena <= cena);
            }


            ViewBag.nazwa = nazwa;
            ViewBag.dataOd = dataOd?.ToString("yyyy-MM-dd");
            ViewBag.dataDo = dataDo?.ToString("yyyy-MM-dd");
            ViewBag.cena = cena;
            return View(await wycieczki.ToListAsync());
        }

        // GET: SzukajWycieczki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wycieczki == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wycieczka == null)
            {
                return NotFound();
            }

            return View(wycieczka);
        }

        [Authorize]
        public async Task<IActionResult> Rezerwuj(int? id)
        {
            if (id == null || _context.Wycieczki == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczki
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wycieczka == null)
            {
                return NotFound();
            }

            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa", id);
            return View("Potwierdz", new UserWycieczka() { WycieczkaId = id.GetValueOrDefault() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Potwierdz([Bind("Id,WycieczkaId,UserId,Imie,Nazwisko")] UserWycieczka userWycieczka)
        {
            if (ModelState.IsValid)
            {
                userWycieczka.UserId = User.GetId();
                _context.Add(userWycieczka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa", userWycieczka.WycieczkaId);
            return View(userWycieczka);
        }

        private bool WycieczkaExists(int id)
        {
            return _context.Wycieczki.Any(e => e.Id == id);
        }
    }
}
