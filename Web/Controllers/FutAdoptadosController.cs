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
using Web.ViewModels;
using Web.Repos.Models;

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
            var adopcionGarritasFelicesContext = _context.FutAdoptados.Include(f => f.Edad).Include(f => f.Enfermedad).Include(f => f.Genero).Include(f => f.Vacuna);
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
                .Include(f => f.Enfermedad)
                .Include(f => f.Genero)
                .Include(f => f.Vacuna)
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
            ViewData["EnfermedadRefId"] = new SelectList(_context.Enfermedades, "Id", "Id");
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id");
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Id");
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
                    ImagemGato = uniqueFileName,
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    GeneroRefId = model.GeneroRefId,
                    VacunaRefId = model.VacunaRefId,
                    EnfermedadRefId = model.EnfermedadRefId,
                    EdadRefId = model.EdadRefId,

                };

                _context.Add(futAdoptado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", model.EdadRefId);
            ViewData["EnfermedadRefId"] = new SelectList(_context.Enfermedades, "Id", "Id", model.EnfermedadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", model.GeneroRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Id", model.VacunaRefId);
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
                Descripcion = futAdoptado.Descripcion,
                FechaRegistro = futAdoptado.FechaRegistro,
                GeneroRefId = futAdoptado.GeneroRefId,
                VacunaRefId = futAdoptado.VacunaRefId,
                EnfermedadRefId = futAdoptado.EnfermedadRefId,
                EdadRefId = futAdoptado.EdadRefId,
            };


            if (futAdoptado == null)
            {
                return NotFound();
            }
            ViewData["EdadRefId"] = new SelectList(_context.Edades, "Id", "Id", futAdoptado.EdadRefId);
            ViewData["EnfermedadRefId"] = new SelectList(_context.Enfermedades, "Id", "Id", futAdoptado.EnfermedadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", futAdoptado.GeneroRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Id", futAdoptado.VacunaRefId);
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

                    futAdoptado.ImagemGato = uniqueFileName;
                    futAdoptado.GeneroRefId = model.GeneroRefId;
                    futAdoptado.Descripcion = model.Descripcion;
                    futAdoptado.FechaRegistro = model.FechaRegistro;
                    futAdoptado.VacunaRefId = model.VacunaRefId;
                    futAdoptado.EdadRefId = model.EdadRefId;
                    futAdoptado.EnfermedadRefId = model.EnfermedadRefId;


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
            ViewData["EnfermedadRefId"] = new SelectList(_context.Enfermedades, "Id", "Id", model.EnfermedadRefId);
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", model.GeneroRefId);
            ViewData["VacunaRefId"] = new SelectList(_context.Vacunas, "Id", "Id", model.VacunaRefId);
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
                .Include(f => f.Enfermedad)
                .Include(f => f.Genero)
                .Include(f => f.Vacuna)
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
