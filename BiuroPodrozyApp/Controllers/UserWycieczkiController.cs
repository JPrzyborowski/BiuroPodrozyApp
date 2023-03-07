using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiuroPodrozyApp.Data;
using BiuroPodrozyApp.Models;

namespace BiuroPodrozyApp.Controllers
{
    public class UserWycieczkiController : Controller
    {
        private readonly BiuroContext _context;

        public UserWycieczkiController(BiuroContext context)
        {
            _context = context;
        }

        // GET: UserWycieczki
        public async Task<IActionResult> Index()
        {
            var biuroContext = _context.UserWycieczki.Include(u => u.Wycieczka);
            return View(await biuroContext.ToListAsync());
        }

        // GET: UserWycieczki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserWycieczki == null)
            {
                return NotFound();
            }

            var userWycieczka = await _context.UserWycieczki
                .Include(u => u.Wycieczka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWycieczka == null)
            {
                return NotFound();
            }

            return View(userWycieczka);
        }

        // GET: UserWycieczki/Create
        public IActionResult Create()
        {
            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa");
            return View();
        }

        // POST: UserWycieczki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WycieczkaId,UserId,Imie,Nazwisko")] UserWycieczka userWycieczka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userWycieczka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa", userWycieczka.WycieczkaId);
            return View(userWycieczka);
        }

        // GET: UserWycieczki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserWycieczki == null)
            {
                return NotFound();
            }

            var userWycieczka = await _context.UserWycieczki.FindAsync(id);
            if (userWycieczka == null)
            {
                return NotFound();
            }
            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa", userWycieczka.WycieczkaId);
            return View(userWycieczka);
        }

        // POST: UserWycieczki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WycieczkaId,UserId,Imie,Nazwisko")] UserWycieczka userWycieczka)
        {
            if (id != userWycieczka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWycieczka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWycieczkaExists(userWycieczka.Id))
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
            ViewData["WycieczkaId"] = new SelectList(_context.Wycieczki, "Id", "Nazwa", userWycieczka.WycieczkaId);
            return View(userWycieczka);
        }

        // GET: UserWycieczki/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserWycieczki == null)
            {
                return NotFound();
            }

            var userWycieczka = await _context.UserWycieczki
                .Include(u => u.Wycieczka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWycieczka == null)
            {
                return NotFound();
            }

            return View(userWycieczka);
        }

        // POST: UserWycieczki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserWycieczki == null)
            {
                return Problem("Entity set 'BiuroContext.UserWycieczki'  is null.");
            }
            var userWycieczka = await _context.UserWycieczki.FindAsync(id);
            if (userWycieczka != null)
            {
                _context.UserWycieczki.Remove(userWycieczka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWycieczkaExists(int id)
        {
          return _context.UserWycieczki.Any(e => e.Id == id);
        }
    }
}
