using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.EF.APP;
using Domain;

namespace WebApp.Controllers
{
    public class CategoryTransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryTransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CategoryTransactions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CategoryTransactions.Include(c => c.Transaction);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CategoryTransactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CategoryTransactions == null)
            {
                return NotFound();
            }

            var categoryTransaction = await _context.CategoryTransactions
                .Include(c => c.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryTransaction == null)
            {
                return NotFound();
            }

            return View(categoryTransaction);
        }

        // GET: CategoryTransactions/Create
        public IActionResult Create()
        {
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "SenderReceiver");
            return View();
        }

        // POST: CategoryTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionId,CategoryId")] CategoryTransaction categoryTransaction)
        {
            if (ModelState.IsValid)
            {
                categoryTransaction.Id = Guid.NewGuid();
                _context.Add(categoryTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "SenderReceiver", categoryTransaction.TransactionId);
            return View(categoryTransaction);
        }

        // GET: CategoryTransactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CategoryTransactions == null)
            {
                return NotFound();
            }

            var categoryTransaction = await _context.CategoryTransactions.FindAsync(id);
            if (categoryTransaction == null)
            {
                return NotFound();
            }
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "SenderReceiver", categoryTransaction.TransactionId);
            return View(categoryTransaction);
        }

        // POST: CategoryTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TransactionId,CategoryId")] CategoryTransaction categoryTransaction)
        {
            if (id != categoryTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryTransactionExists(categoryTransaction.Id))
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
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "SenderReceiver", categoryTransaction.TransactionId);
            return View(categoryTransaction);
        }

        // GET: CategoryTransactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CategoryTransactions == null)
            {
                return NotFound();
            }

            var categoryTransaction = await _context.CategoryTransactions
                .Include(c => c.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryTransaction == null)
            {
                return NotFound();
            }

            return View(categoryTransaction);
        }

        // POST: CategoryTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CategoryTransactions == null)
            {
                return Problem("Entity set 'AppDbContext.CategoryTransactions'  is null.");
            }
            var categoryTransaction = await _context.CategoryTransactions.FindAsync(id);
            if (categoryTransaction != null)
            {
                _context.CategoryTransactions.Remove(categoryTransaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryTransactionExists(Guid id)
        {
          return (_context.CategoryTransactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
