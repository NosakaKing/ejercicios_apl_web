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
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stock.Include(s => s.Product).Include(s => s.Proveedor);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST STOCK
        public Boolean controlarStock(int id, int cantidad)
        {
           var cn = _context.Stock.FirstOrDefault(st => st.Id == id);
            if (cn ==  null)
            {
                return false;
            }
            if (cn.Cantidad >= cantidad)
            {
                return true;
            }
            else
            {
                return false;
            }

            }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModel = await _context.Stock
                .Include(s => s.Product)
                .Include(s => s.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return View(stockModel);
        }

        // GET: Stock/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Productos, "Id", "NombreProducto");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "CorreoElectronico");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,FechaFabricacion,FechaVencimiento,FechaIngreso,ProductId,ProveedorId")] StockModel stockModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Productos, "Id", "Id", stockModel.ProductId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "CorreoElectronico", stockModel.ProveedorId);
            return View(stockModel);
        }

        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModel = await _context.Stock.FindAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Productos, "Id", "NombreProducto", stockModel.ProductId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "CorreoElectronico", stockModel.ProveedorId);
            return View(stockModel);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,FechaFabricacion,FechaVencimiento,FechaIngreso,ProductId,ProveedorId")] StockModel stockModel)
        {
            if (id != stockModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockModelExists(stockModel.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Productos, "Id", "Id", stockModel.ProductId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "CorreoElectronico", stockModel.ProveedorId);
            return View(stockModel);
        }

        // GET: Stock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModel = await _context.Stock
                .Include(s => s.Product)
                .Include(s => s.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return View(stockModel);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockModel = await _context.Stock.FindAsync(id);
            if (stockModel != null)
            {
                _context.Stock.Remove(stockModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockModelExists(int id)
        {
            return _context.Stock.Any(e => e.Id == id);
        }
    }
}
