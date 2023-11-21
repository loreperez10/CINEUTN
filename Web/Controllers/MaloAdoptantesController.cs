using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System;
using Web.Repos.Models;

namespace Web.Controllers
{
    public class MaloAdoptantesController : Controller
    {
        private readonly AdopcionGarritasFelicesContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MaloAdoptantesController(AdopcionGarritasFelicesContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

        public IActionResult Importar()
        {

            return View();
        }

        [HttpPost, ActionName("MostrarDatos")]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }

                ISheet HojaExcel = MiExcel.GetSheetAt(0);

                int cantidadFilas = HojaExcel.LastRowNum;

                List<MaloAdoptante> lista = new List<MaloAdoptante>();

                for (int i = 1; i <= cantidadFilas; i++)
                {

                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new MaloAdoptante
                    {
                        NombreyApellido = fila.GetCell(0).ToString(),
                        Direccion = fila.GetCell(0).ToString(),                      
                        FechaRegistro = DateTime.Now

                    });
                }

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            else
            {

                return View();
            }

        }

        [HttpPost, ActionName("EnviarDatos")]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }

                ISheet HojaExcel = MiExcel.GetSheetAt(0);

                int cantidadFilas = HojaExcel.LastRowNum;
                List<MaloAdoptante> lista = new List<MaloAdoptante>();

                for (int i = 1; i <= cantidadFilas; i++)
                {

                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new MaloAdoptante
                    {
                        NombreyApellido = fila.GetCell(0).ToString(),
                        Direccion = fila.GetCell(0).ToString(),
                        FechaRegistro = DateTime.Now

                    });
                }

                _context.BulkInsert(lista);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            else
            {
                return View();
            }

        }

        public IActionResult DownloadFile()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "archivos", "MaloAdopante.xlsx");
            return File(System.IO.File.ReadAllBytes(filepath), "application/vnd.ms-excel", System.IO.Path.GetFileName(filepath));
        }

    }
}
