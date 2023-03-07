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
    [Authorize(Roles = "Administrator")]
    public class WycieczkiController : Controller
    {
        private readonly BiuroContext _context;

        public WycieczkiController(BiuroContext context)
        {
            _context = context;
        }

        // GET: Wycieczki
        public async Task<IActionResult> Index()
        {
              return View(await _context.Wycieczki.ToListAsync());
        }

        // GET: Wycieczki/Details/5
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

        // GET: Wycieczki/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wycieczki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,DataOd,DataDo,Opis,Cena")] Wycieczka wycieczka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wycieczka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wycieczka);
        }

        // GET: Wycieczki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wycieczki == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczki.FindAsync(id);
            if (wycieczka == null)
            {
                return NotFound();
            }
            return View(wycieczka);
        }

        // POST: Wycieczki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,DataOd,DataDo,Opis,Cena")] Wycieczka wycieczka)
        {
            if (id != wycieczka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wycieczka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WycieczkaExists(wycieczka.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wycieczka);
        }

        // GET: Wycieczki/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Wycieczki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wycieczki == null)
            {
                return Problem("Entity set 'BiuroContext.Wycieczki'  is null.");
            }
            var wycieczka = await _context.Wycieczki.FindAsync(id);
            if (wycieczka != null)
            {
                _context.Wycieczki.Remove(wycieczka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WycieczkaExists(int id)
        {
          return _context.Wycieczki.Any(e => e.Id == id);
        }
    }
}
