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
    public class FinancialCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public FinancialCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FinancialCategories
        public async Task<IActionResult> Index()
        {
              return _context.FinancialCategories != null ? 
                          View(await _context.FinancialCategories.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.FinancialCategories'  is null.");
        }

        // GET: FinancialCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FinancialCategories == null)
            {
                return NotFound();
            }

            var financialCategory = await _context.FinancialCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialCategory == null)
            {
                return NotFound();
            }

            return View(financialCategory);
        }

        // GET: FinancialCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinancialCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HexColor,Icon")] FinancialCategory financialCategory)
        {
            if (ModelState.IsValid)
            {
                financialCategory.Id = Guid.NewGuid();
                _context.Add(financialCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financialCategory);
        }

        // GET: FinancialCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FinancialCategories == null)
            {
                return NotFound();
            }

            var financialCategory = await _context.FinancialCategories.FindAsync(id);
            if (financialCategory == null)
            {
                return NotFound();
            }
            return View(financialCategory);
        }

        // POST: FinancialCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,HexColor,Icon")] FinancialCategory financialCategory)
        {
            if (id != financialCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialCategoryExists(financialCategory.Id))
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
            return View(financialCategory);
        }

        // GET: FinancialCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FinancialCategories == null)
            {
                return NotFound();
            }

            var financialCategory = await _context.FinancialCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialCategory == null)
            {
                return NotFound();
            }

            return View(financialCategory);
        }

        // POST: FinancialCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FinancialCategories == null)
            {
                return Problem("Entity set 'AppDbContext.FinancialCategories'  is null.");
            }
            var financialCategory = await _context.FinancialCategories.FindAsync(id);
            if (financialCategory != null)
            {
                _context.FinancialCategories.Remove(financialCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialCategoryExists(Guid id)
        {
          return (_context.FinancialCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
