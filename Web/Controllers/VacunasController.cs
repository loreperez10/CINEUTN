using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Repos;
using Web.Repos.Models;

namespace Web.Controllers
{
    public class VacunasController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;

        public VacunasController(AdopcionGarritasFelicesContext context)
        {
            _context = context;
        }

        // GET: Vacunas
        public async Task<IActionResult> Index()
        {
              return _context.Vacunas != null ? 
                          View(await _context.Vacunas.ToListAsync()) :
                          Problem("Entity set 'AdopcionGarritasFelicesContext.Vacunas'  is null.");
        }

        // GET: Vacunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // GET: Vacunas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacunas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Vacuna vacuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacuna);
        }

        // GET: Vacunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null)
            {
                return NotFound();
            }
            return View(vacuna);
        }

        // POST: Vacunas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Vacuna vacuna)
        {
            if (id != vacuna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunaExists(vacuna.Id))
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
            return View(vacuna);
        }

        // GET: Vacunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // POST: Vacunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacunas == null)
            {
                return Problem("Entity set 'AdopcionGarritasFelicesContext.Vacunas'  is null.");
            }
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna != null)
            {
                _context.Vacunas.Remove(vacuna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacunaExists(int id)
        {
          return (_context.Vacunas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
