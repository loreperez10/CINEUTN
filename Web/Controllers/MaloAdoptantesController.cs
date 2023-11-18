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
    public class MaloAdoptantesController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;

        public MaloAdoptantesController(AdopcionGarritasFelicesContext context)
        {
            _context = context;
        }

        // GET: MaloAdoptantes
        public async Task<IActionResult> Index()
        {
              return _context.MaloAdoptantes != null ? 
                          View(await _context.MaloAdoptantes.ToListAsync()) :
                          Problem("Entity set 'AdopcionGarritasFelicesContext.MaloAdoptantes'  is null.");
        }

        // GET: MaloAdoptantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaloAdoptantes == null)
            {
                return NotFound();
            }

            var maloAdoptante = await _context.MaloAdoptantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maloAdoptante == null)
            {
                return NotFound();
            }

            return View(maloAdoptante);
        }

        // GET: MaloAdoptantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaloAdoptantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreyApellido,Direccion,FechaRegistro")] MaloAdoptante maloAdoptante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maloAdoptante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maloAdoptante);
        }

        // GET: MaloAdoptantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaloAdoptantes == null)
            {
                return NotFound();
            }

            var maloAdoptante = await _context.MaloAdoptantes.FindAsync(id);
            if (maloAdoptante == null)
            {
                return NotFound();
            }
            return View(maloAdoptante);
        }

        // POST: MaloAdoptantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreyApellido,Direccion,FechaRegistro")] MaloAdoptante maloAdoptante)
        {
            if (id != maloAdoptante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maloAdoptante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaloAdoptanteExists(maloAdoptante.Id))
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
            return View(maloAdoptante);
        }

        // GET: MaloAdoptantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaloAdoptantes == null)
            {
                return NotFound();
            }

            var maloAdoptante = await _context.MaloAdoptantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maloAdoptante == null)
            {
                return NotFound();
            }

            return View(maloAdoptante);
        }

        // POST: MaloAdoptantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaloAdoptantes == null)
            {
                return Problem("Entity set 'AdopcionGarritasFelicesContext.MaloAdoptantes'  is null.");
            }
            var maloAdoptante = await _context.MaloAdoptantes.FindAsync(id);
            if (maloAdoptante != null)
            {
                _context.MaloAdoptantes.Remove(maloAdoptante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaloAdoptanteExists(int id)
        {
          return (_context.MaloAdoptantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
