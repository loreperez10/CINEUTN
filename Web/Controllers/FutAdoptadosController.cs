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
    public class FutAdoptadosController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;

        public FutAdoptadosController(AdopcionGarritasFelicesContext context)
        {
            _context = context;
        }

        // GET: FutAdoptados
        public async Task<IActionResult> Index()
        {
            var adopcionGarritasFelicesContext = _context.FutAdoptados.Include(f => f.Edad).Include(f => f.Genero);
            return View(await adopcionGarritasFelicesContext.ToListAsync());
        }

        // GET: FutAdoptados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FutAdoptados == null)
            {
                return NotFound();
            }

            var futAdoptado = await _context.FutAdoptados
                .Include(f => f.Edad)
                .Include(f => f.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (futAdoptado == null)
            {
                return NotFound();
            }

            return View(futAdoptado);
        }

        // GET: FutAdoptados/Create
        public IActionResult Create()
        {
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id");
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id");
            return View();
        }

        // POST: FutAdoptados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ImagemPelicula,Clasificacion,GeneroRefId,EdadRefId,VacunaRefId,FechaRegistro")] FutAdoptado futAdoptado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(futAdoptado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", futAdoptado.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", futAdoptado.GeneroRefId);
            return View(futAdoptado);
        }

        // GET: FutAdoptados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FutAdoptados == null)
            {
                return NotFound();
            }

            var futAdoptado = await _context.FutAdoptados.FindAsync(id);
            if (futAdoptado == null)
            {
                return NotFound();
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", futAdoptado.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", futAdoptado.GeneroRefId);
            return View(futAdoptado);
        }

        // POST: FutAdoptados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,ImagemPelicula,Clasificacion,GeneroRefId,EdadRefId,VacunaRefId,FechaRegistro")] FutAdoptado futAdoptado)
        {
            if (id != futAdoptado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(futAdoptado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutAdoptadoExists(futAdoptado.Id))
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
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", futAdoptado.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", futAdoptado.GeneroRefId);
            return View(futAdoptado);
        }

        // GET: FutAdoptados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FutAdoptados == null)
            {
                return NotFound();
            }

            var futAdoptado = await _context.FutAdoptados
                .Include(f => f.Edad)
                .Include(f => f.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (futAdoptado == null)
            {
                return NotFound();
            }

            return View(futAdoptado);
        }

        // POST: FutAdoptados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FutAdoptados == null)
            {
                return Problem("Entity set 'AdopcionGarritasFelicesContext.FutAdoptados'  is null.");
            }
            var futAdoptado = await _context.FutAdoptados.FindAsync(id);
            if (futAdoptado != null)
            {
                _context.FutAdoptados.Remove(futAdoptado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FutAdoptadoExists(int id)
        {
          return (_context.FutAdoptados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
