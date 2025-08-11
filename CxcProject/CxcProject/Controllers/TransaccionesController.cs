using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CxcProject.Data;
using CxcProject.Models;

namespace CxcProject.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly CxcDbContext _context;

        public TransaccionesController(CxcDbContext context)
        {
            _context = context;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            var cxcDbContext = _context.Transacciones.Include(t => t.Cliente).Include(t => t.TipoDocumento);
            return View(await cxcDbContext.ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones
                .Include(t => t.Cliente)
                .Include(t => t.TipoDocumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre");
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos.Where(t => t.Estado), "Id", "Descripcion");
            return View();
        }

        // POST: Transacciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoMovimiento,TipoDocumentoId,NumeroDocumento,Fecha,ClienteId,Monto")] Transaccion transaccion)
        {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", transaccion.ClienteId);
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos.Where(t => t.Estado), "Id", "Descripcion", transaccion.TipoDocumentoId);
            return View(transaccion);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", transaccion.ClienteId);
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos.Where(t => t.Estado), "Id", "Descripcion", transaccion.TipoDocumentoId);
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoMovimiento,TipoDocumentoId,NumeroDocumento,Fecha,ClienteId,Monto")] Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", transaccion.ClienteId);
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos.Where(t => t.Estado), "Id", "Descripcion", transaccion.TipoDocumentoId);
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones
                .Include(t => t.Cliente)
                .Include(t => t.TipoDocumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transacciones.Remove(transaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transacciones.Any(e => e.Id == id);
        }
    }
}
