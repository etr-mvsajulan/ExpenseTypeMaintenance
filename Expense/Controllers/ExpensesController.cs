using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense.DAL;
using Expense.Models.DBEntities;
using Expense.Models;
using Microsoft.Data.SqlClient;

namespace Expense.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseDBContext _context;

        public ExpensesController(ExpenseDBContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index(string search)
        {

            var expense = _context.Expense.Where(x => string.IsNullOrEmpty(search) || x.TransactionNumber.Contains(search)).Select(x => new ExpenseViewModel
            {
                ExpenseId = x.ExpenseId,
                TransactionNumber = x.TransactionNumber,
                TransactionDate = x.TransactionDate,
                CostUnitCode = x.CostUnitCode,
                CostUnitName = x.CostUnitName,
                Remarks = x.Remarks,
                TaxableTotal = x.TaxableTotal,
                VatTotal = x.VatTotal,
                NetOfVatTotal = x.NetOfVatTotal,
                Status = (ExpenseViewModel.ExpenseStatus)x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
            });

            return expense != null ? View(await expense.ToListAsync()) : Problem("Entity set 'ExpenseDBContext.Expense'  is null.");

              //return _context.Expense != null ? 
                          //View(await _context.Expense.ToListAsync()) :
                          //Problem("Entity set 'ExpenseDBContext.Expense'  is null.");
            //return await _expenseService.GetExpensesAsync();
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expense == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseCode"] = GenerateCode(); 
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,TransactionNumber,TransactionDate,CostUnitCode,CostUnitName,Remarks,TaxableTotal,VatTotal,NetOfVatTotal,Status,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Expense.Models.DBEntities.Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var expenseView = _context.Expense.Find(id);
            if (id == null || expenseView == null)
            {
                return NotFound();
            }

            var expenseViewModel = _context.Expense.Where(x=> x.ExpenseId == id).Select(x=> new ExpenseViewModel
            {
                ExpenseId = x.ExpenseId,
                TransactionNumber = x.TransactionNumber,
                TransactionDate = x.TransactionDate,
                CostUnitCode = x.CostUnitCode,
                CostUnitName = x.CostUnitName,
                Remarks = x.Remarks,
                TaxableTotal = x.TaxableTotal,
                VatTotal = x.VatTotal,
                NetOfVatTotal = x.NetOfVatTotal,
                Status = (ExpenseViewModel.ExpenseStatus)x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate
            }).FirstOrDefault();
            if (expenseViewModel == null)
            {
                return NotFound();
            }
            return View(expenseViewModel);
        }

        // POST: Expenses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateExpenseViewModel expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingExpense = _context.Expense.Find(expense.ExpenseId);
                    if (existingExpense != null)
                    {
                        existingExpense.TransactionDate = expense.TransactionDate;
                        existingExpense.CostUnitCode = expense.CostUnitCode;
                        existingExpense.CostUnitName = expense.CostUnitName;
                        existingExpense.Remarks = expense.Remarks;
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;

                    }
                }
                
            }
            return View();
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expense == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expense == null)
            {
                return Problem("Entity set 'ExpenseDBContext.Expense'  is null.");
            }
            var expense = await _context.Expense.FindAsync(id);
            if (expense != null)
            {
                _context.Expense.Remove(expense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
          return (_context.Expense?.Any(e => e.ExpenseId == id)).GetValueOrDefault();
        }

        public string GenerateCode()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR ExpenseTransactions)", result);

            int values = (int)result.Value;
            string NextSequence = values.ToString("D10");
            return NextSequence;
        }

        public ExpensetypeViewModel GetExpenseTypeById(int? ID)
        {
            throw new NotImplementedException();
        }
    }
}
