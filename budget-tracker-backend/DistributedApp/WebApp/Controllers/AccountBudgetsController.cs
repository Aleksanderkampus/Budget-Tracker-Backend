using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.EF.APP;
using Domain;

namespace WebApp.Controllers
{
    public class AccountBudgetsController : Controller
    {
        private readonly AppDbContext _context;

        public AccountBudgetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AccountBudgets
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AccountBudgets.Include(a => a.Account).Include(a => a.Budget);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AccountBudgets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AccountBudgets == null)
            {
                return NotFound();
            }

            var accountBudget = await _context.AccountBudgets
                .Include(a => a.Account)
                .Include(a => a.Budget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountBudget == null)
            {
                return NotFound();
            }

            return View(accountBudget);
        }

        // GET: AccountBudgets/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Bank");
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name");
            return View();
        }

        // POST: AccountBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,BudgetId")] AccountBudget accountBudget)
        {
            if (ModelState.IsValid)
            {
                accountBudget.Id = Guid.NewGuid();
                _context.Add(accountBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Bank", accountBudget.AccountId);
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", accountBudget.BudgetId);
            return View(accountBudget);
        }

        // GET: AccountBudgets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AccountBudgets == null)
            {
                return NotFound();
            }

            var accountBudget = await _context.AccountBudgets.FindAsync(id);
            if (accountBudget == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Bank", accountBudget.AccountId);
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", accountBudget.BudgetId);
            return View(accountBudget);
        }

        // POST: AccountBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AccountId,BudgetId")] AccountBudget accountBudget)
        {
            if (id != accountBudget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountBudgetExists(accountBudget.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Bank", accountBudget.AccountId);
            ViewData["BudgetId"] = new SelectList(_context.Budgets, "Id", "Name", accountBudget.BudgetId);
            return View(accountBudget);
        }

        // GET: AccountBudgets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AccountBudgets == null)
            {
                return NotFound();
            }

            var accountBudget = await _context.AccountBudgets
                .Include(a => a.Account)
                .Include(a => a.Budget)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountBudget == null)
            {
                return NotFound();
            }

            return View(accountBudget);
        }

        // POST: AccountBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AccountBudgets == null)
            {
                return Problem("Entity set 'AppDbContext.AccountBudgets'  is null.");
            }
            var accountBudget = await _context.AccountBudgets.FindAsync(id);
            if (accountBudget != null)
            {
                _context.AccountBudgets.Remove(accountBudget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountBudgetExists(Guid id)
        {
          return (_context.AccountBudgets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
