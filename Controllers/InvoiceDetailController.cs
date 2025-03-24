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
    public class InvoiceDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceDetail.Include(i => i.InvoiceModel).Include(i => i.ProductModel).Include(i => i.StockModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetailModel = await _context.InvoiceDetail
                .Include(i => i.InvoiceModel)
                .Include(i => i.ProductModel)
                .Include(i => i.StockModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetailModel == null)
            {
                return NotFound();
            }

            return View(invoiceDetailModel);
        }

        // GET: InvoiceDetail/Create
        public IActionResult Create()
        {
            ViewData["InvoiceModelId"] = new SelectList(_context.Invoice, "Id", "Id");
            ViewData["ProductModelId"] = new SelectList(_context.Productos, "Id", "Id");
            ViewData["StockModelId"] = new SelectList(_context.Stock, "Id", "Id");
            return View();
        }

        // POST: InvoiceDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,Valor,ProductModelId,InvoiceModelId,StockModelId")] InvoiceDetailModel invoiceDetailModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetailModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceModelId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetailModel.InvoiceModelId);
            ViewData["ProductModelId"] = new SelectList(_context.Productos, "Id", "Id", invoiceDetailModel.ProductModelId);
            ViewData["StockModelId"] = new SelectList(_context.Stock, "Id", "Id", invoiceDetailModel.StockModelId);
            return View(invoiceDetailModel);
        }

        // GET: InvoiceDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetailModel = await _context.InvoiceDetail.FindAsync(id);
            if (invoiceDetailModel == null)
            {
                return NotFound();
            }
            ViewData["InvoiceModelId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetailModel.InvoiceModelId);
            ViewData["ProductModelId"] = new SelectList(_context.Productos, "Id", "Id", invoiceDetailModel.ProductModelId);
            ViewData["StockModelId"] = new SelectList(_context.Stock, "Id", "Id", invoiceDetailModel.StockModelId);
            return View(invoiceDetailModel);
        }

        // POST: InvoiceDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,Valor,ProductModelId,InvoiceModelId,StockModelId")] InvoiceDetailModel invoiceDetailModel)
        {
            if (id != invoiceDetailModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetailModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailModelExists(invoiceDetailModel.Id))
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
            ViewData["InvoiceModelId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetailModel.InvoiceModelId);
            ViewData["ProductModelId"] = new SelectList(_context.Productos, "Id", "Id", invoiceDetailModel.ProductModelId);
            ViewData["StockModelId"] = new SelectList(_context.Stock, "Id", "Id", invoiceDetailModel.StockModelId);
            return View(invoiceDetailModel);
        }

        // GET: InvoiceDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetailModel = await _context.InvoiceDetail
                .Include(i => i.InvoiceModel)
                .Include(i => i.ProductModel)
                .Include(i => i.StockModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetailModel == null)
            {
                return NotFound();
            }

            return View(invoiceDetailModel);
        }

        // POST: InvoiceDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceDetailModel = await _context.InvoiceDetail.FindAsync(id);
            if (invoiceDetailModel != null)
            {
                _context.InvoiceDetail.Remove(invoiceDetailModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailModelExists(int id)
        {
            return _context.InvoiceDetail.Any(e => e.Id == id);
        }
    }
}
