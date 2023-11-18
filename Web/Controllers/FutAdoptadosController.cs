using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;
using Web.Repos.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class FutAdoptadosController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FutAdoptadosController(AdopcionGarritasFelicesContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(FutAdoptadoViewModels model)
        {
            string uniqueFileName = UploadedFile(model);

            if (ModelState.IsValid)
            {
                FutAdoptado futAdoptado = new FutAdoptado()
                {
                    ImagemPelicula = uniqueFileName,
                    Clasificacion = model.Clasificacion,
                    Descripcion = model.Descripcion,                              
                    FechaRegistro = model.FechaRegistro,
                    GeneroRefId = model.GeneroRefId,
                    EdadRefId = model.EdadRefId,               

                };
                _context.Add(futAdoptado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", model.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", model.GeneroRefId);
            return View(model);
        }
        private string UploadedFile(FutAdoptadoViewModels model)
        {
            string uniqueFileName = null;

            if (model.Imagem != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagem.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: FutAdoptados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FutAdoptados == null)
            {
                return NotFound();
            }

            var futAdoptado = await _context.FutAdoptados.FindAsync(id);

            FutAdoptadoViewModels futAdoptadoViewModel = new FutAdoptadoViewModels()
            {
                
                Clasificacion = futAdoptado.Clasificacion,
                Descripcion = futAdoptado.Descripcion,
                FechaRegistro = futAdoptado.FechaRegistro,
                GeneroRefId = futAdoptado.GeneroRefId,
                EdadRefId = futAdoptado.EdadRefId,

            };

            if (futAdoptado == null)
            {
                return NotFound();
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", futAdoptado.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", futAdoptado.GeneroRefId);
            return View(futAdoptadoViewModel);
        }

        // POST: FutAdoptados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FutAdoptadoViewModels model)
        {
            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var futAdoptado = await _context.FutAdoptados.FindAsync(id);


                    futAdoptado.ImagemPelicula = uniqueFileName;
                    futAdoptado.Clasificacion = model.Clasificacion;
                    futAdoptado.Descripcion = model.Descripcion;
                    futAdoptado.FechaRegistro = model.FechaRegistro;
                    futAdoptado.GeneroRefId = model.GeneroRefId;
                    futAdoptado.EdadRefId = model.EdadRefId;
                   

                    _context.Update(futAdoptado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutAdoptadoExists(model.Id))
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
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", model.EdadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", model.GeneroRefId);
            return View(model);
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
