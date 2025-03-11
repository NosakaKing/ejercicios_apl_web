using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.Data;
using SistemaInventario.Models;

namespace SistemaInventario.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProveedorModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedor.ToListAsync());
        }

        // GET: ProveedorModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorModel = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedorModel == null)
            {
                return NotFound();
            }

            return View(proveedorModel);
        }

        // GET: ProveedorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProveedorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProveedor,Direccion,Telefono,CorreoElectronico")] ProveedorModel proveedorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedorModel);
        }

        // GET: ProveedorModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorModel = await _context.Proveedor.FindAsync(id);
            if (proveedorModel == null)
            {
                return NotFound();
            }
            return View(proveedorModel);
        }

        // POST: ProveedorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProveedor,Direccion,Telefono,CorreoElectronico")] ProveedorModel proveedorModel)
        {
            if (id != proveedorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorModelExists(proveedorModel.Id))
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
            return View(proveedorModel);
        }

        // GET: ProveedorModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorModel = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedorModel == null)
            {
                return NotFound();
            }

            return View(proveedorModel);
        }

        // POST: ProveedorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedorModel = await _context.Proveedor.FindAsync(id);
            if (proveedorModel != null)
            {
                _context.Proveedor.Remove(proveedorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorModelExists(int id)
        {
            return _context.Proveedor.Any(e => e.Id == id);
        }
    }
}
