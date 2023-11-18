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
    public class EnfermedadesController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;

        public EnfermedadesController(AdopcionGarritasFelicesContext context)
        {
            _context = context;
        }

        // GET: Enfermedades
        public async Task<IActionResult> Index()
        {
              return _context.Enfermedades != null ? 
                          View(await _context.Enfermedades.ToListAsync()) :
                          Problem("Entity set 'AdopcionGarritasFelicesContext.Enfermedades'  is null.");
        }

        // GET: Enfermedades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enfermedades == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // GET: Enfermedades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermedades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Enfermedad enfermedad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermedad);
        }

        // GET: Enfermedades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enfermedades == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades.FindAsync(id);
            if (enfermedad == null)
            {
                return NotFound();
            }
            return View(enfermedad);
        }

        // POST: Enfermedades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Enfermedad enfermedad)
        {
            if (id != enfermedad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadExists(enfermedad.Id))
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
            return View(enfermedad);
        }

        // GET: Enfermedades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enfermedades == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // POST: Enfermedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enfermedades == null)
            {
                return Problem("Entity set 'AdopcionGarritasFelicesContext.Enfermedades'  is null.");
            }
            var enfermedad = await _context.Enfermedades.FindAsync(id);
            if (enfermedad != null)
            {
                _context.Enfermedades.Remove(enfermedad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadExists(int id)
        {
          return (_context.Enfermedades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
