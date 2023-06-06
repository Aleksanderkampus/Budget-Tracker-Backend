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
    public class CategoryBudgetsController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryBudgetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CategoryBudgets
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CategoryBudgets.Include(c => c.Budget);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CategoryBudgets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CategoryBudgets == null)
            {
                return NotFound();
            }

            var categoryBudget = await _context.CategoryBudgets
                .Include(c => c.Budget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryBudget == null)
            {
                return NotFound();
            }

            return View(categoryBudget);
        }

        // GET: CategoryBudgets/Create
        public IActionResult Create()
        {
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name");
            return View();
        }

        // POST: CategoryBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BudgetId,CategoryId")] CategoryBudget categoryBudget)
        {
            if (ModelState.IsValid)
            {
                categoryBudget.Id = Guid.NewGuid();
                _context.Add(categoryBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", categoryBudget.BudgetId);
            return View(categoryBudget);
        }

        // GET: CategoryBudgets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CategoryBudgets == null)
            {
                return NotFound();
            }

            var categoryBudget = await _context.CategoryBudgets.FindAsync(id);
            if (categoryBudget == null)
            {
                return NotFound();
            }
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", categoryBudget.BudgetId);
            return View(categoryBudget);
        }

        // POST: CategoryBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BudgetId,CategoryId")] CategoryBudget categoryBudget)
        {
            if (id != categoryBudget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryBudgetExists(categoryBudget.Id))
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
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", categoryBudget.BudgetId);
            return View(categoryBudget);
        }

        // GET: CategoryBudgets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CategoryBudgets == null)
            {
                return NotFound();
            }

            var categoryBudget = await _context.CategoryBudgets
                .Include(c => c.Budget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryBudget == null)
            {
                return NotFound();
            }

            return View(categoryBudget);
        }

        // POST: CategoryBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CategoryBudgets == null)
            {
                return Problem("Entity set 'AppDbContext.CategoryBudgets'  is null.");
            }
            var categoryBudget = await _context.CategoryBudgets.FindAsync(id);
            if (categoryBudget != null)
            {
                _context.CategoryBudgets.Remove(categoryBudget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryBudgetExists(Guid id)
        {
          return (_context.CategoryBudgets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
