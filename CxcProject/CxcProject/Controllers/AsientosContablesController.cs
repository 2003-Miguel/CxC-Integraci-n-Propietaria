using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CxcProject.Data;
using CxcProject.Models;
using CxcProject.Interfaces;

namespace CxcProject.Controllers
{
    public class AsientosContablesController : Controller
    {
        private readonly CxcDbContext _context;
        private readonly IContabilidadService _contabilidadService;

        public AsientosContablesController(CxcDbContext context, IContabilidadService contabilidadService)
        {
            _context = context;
            _contabilidadService = contabilidadService;
        }

        private SelectList ObtenerCuentasSelectList(int? selectedValue = null)
        {
            var cuentas = new List<SelectListItem>
    {
        new SelectListItem("2 - ACTIVOS FIJOS", "2"),
        new SelectListItem("3 - CAJA CHICA", "3"),
        new SelectListItem("4 - CUENTA CORRIENTE BANCO X", "4"),
        new SelectListItem("6 - INVENTARIO", "6"),
        new SelectListItem("8 - CUENTAS X COBRAR CLIENTE X", "8"),
        new SelectListItem("13 - INGRESOS Y VENTAS", "13")
        };

            return new SelectList(cuentas, "Value", "Text", selectedValue);
        }

        public async Task<IActionResult> Entradas()
        {
            var json = await _contabilidadService.ObtenerEntradasContablesAsync();
            if (string.IsNullOrEmpty(json))
            {
                TempData["Error"] = "No se pudieron obtener las entradas contables.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.JsonEntradas = json;

            return View();
        }

        // GET: AsientosContables
        public async Task<IActionResult> Index()
        {
            var asientos = await _context.AsientosContables.Include(a => a.Cliente).ToListAsync();
            return View(asientos);
        }

        // POST: AsientosContables/Enviar/5
        [HttpPost]
        public async Task<IActionResult> Enviar(int id)
        {
            var asiento = await _context.AsientosContables.FindAsync(id);
            if (asiento == null)
                return NotFound();

            bool enviado = await _contabilidadService.EnviarAsientoContableAsync(asiento);

            if (enviado)
                TempData["Success"] = $"Asiento {id} enviado exitosamente.";
            else
                TempData["Error"] = $"Error al enviar el asiento {id}.";

            return RedirectToAction(nameof(Index));
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
            ViewData["Cuenta"] = ObtenerCuentasSelectList();
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
            ViewData["Cuenta"] = ObtenerCuentasSelectList();
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
