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
    public class AsientosContablesController : Controller
    {
        private readonly CxcDbContext _context;

        public AsientosContablesController(CxcDbContext context)
        {
            _context = context;
        }

        // GET: AsientosContables
        public async Task<IActionResult> Index()
        {
            var cxcDbContext = _context.AsientosContables.Include(a => a.Cliente);
            return View(await cxcDbContext.ToListAsync());
        }

        // GET: AsientosContables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // GET: AsientosContables/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre");
            return View();
        }

        // POST: AsientosContables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ClienteId,Cuenta,TipoMovimiento,Fecha,Monto,Estado")] AsientoContable asientoContable)
        {
                _context.Add(asientoContable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", asientoContable.ClienteId);
            return View(asientoContable);
        }

        // GET: AsientosContables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables.FindAsync(id);
            if (asientoContable == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", asientoContable.ClienteId);
            return View(asientoContable);
        }

        // POST: AsientosContables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,ClienteId,Cuenta,TipoMovimiento,Fecha,Monto,Estado")] AsientoContable asientoContable)
        {
            if (id != asientoContable.Id)
            {
                return NotFound();
            }
            
                try
                {
                    _context.Update(asientoContable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoContableExists(asientoContable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            ViewData["ClienteId"] = new SelectList(_context.Clientes.Where(c => c.Estado), "Id", "Nombre", asientoContable.ClienteId);
            return View(asientoContable);
        }

        // GET: AsientosContables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // POST: AsientosContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asientoContable = await _context.AsientosContables.FindAsync(id);
            if (asientoContable != null)
            {
                _context.AsientosContables.Remove(asientoContable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoContableExists(int id)
        {
            return _context.AsientosContables.Any(e => e.Id == id);
        }
    }
}
