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
    public class FutAdoptantesController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;

        public FutAdoptantesController(AdopcionGarritasFelicesContext context)
        {
            _context = context;
        }

        // GET: FutAdoptantes
        public async Task<IActionResult> Index()
        {
              return _context.FutAdoptantes != null ? 
                          View(await _context.FutAdoptantes.ToListAsync()) :
                          Problem("Entity set 'AdopcionGarritasFelicesContext.FutAdoptantes'  is null.");
        }

        // GET: FutAdoptantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FutAdoptantes == null)
            {
                return NotFound();
            }

            var futAdoptante = await _context.FutAdoptantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (futAdoptante == null)
            {
                return NotFound();
            }

            return View(futAdoptante);
        }

        // GET: FutAdoptantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FutAdoptantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreyApellido,Contacto,Interes,FechaRegistro")] FutAdoptante futAdoptante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(futAdoptante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(futAdoptante);
        }

        // GET: FutAdoptantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FutAdoptantes == null)
            {
                return NotFound();
            }

            var futAdoptante = await _context.FutAdoptantes.FindAsync(id);
            if (futAdoptante == null)
            {
                return NotFound();
            }
            return View(futAdoptante);
        }

        // POST: FutAdoptantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreyApellido,Contacto,Interes,FechaRegistro")] FutAdoptante futAdoptante)
        {
            if (id != futAdoptante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(futAdoptante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutAdoptanteExists(futAdoptante.Id))
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
            return View(futAdoptante);
        }

        // GET: FutAdoptantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FutAdoptantes == null)
            {
                return NotFound();
            }

            var futAdoptante = await _context.FutAdoptantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (futAdoptante == null)
            {
                return NotFound();
            }

            return View(futAdoptante);
        }

        // POST: FutAdoptantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FutAdoptantes == null)
            {
                return Problem("Entity set 'AdopcionGarritasFelicesContext.FutAdoptantes'  is null.");
            }
            var futAdoptante = await _context.FutAdoptantes.FindAsync(id);
            if (futAdoptante != null)
            {
                _context.FutAdoptantes.Remove(futAdoptante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FutAdoptanteExists(int id)
        {
          return (_context.FutAdoptantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
