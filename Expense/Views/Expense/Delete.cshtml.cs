using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Expense.DAL;
using Expense.Models;

namespace Expense.Views.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly Expense.DAL.ExpenseDBContext _context;

        public DeleteModel(Expense.DAL.ExpenseDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ExpenseViewModel ExpenseViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ExpenseViewModel == null)
            {
                return NotFound();
            }

            var expenseviewmodel = await _context.ExpenseViewModel.FirstOrDefaultAsync(m => m.ExpenseId == id);

            if (expenseviewmodel == null)
            {
                return NotFound();
            }
            else 
            {
                ExpenseViewModel = expenseviewmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ExpenseViewModel == null)
            {
                return NotFound();
            }
            var expenseviewmodel = await _context.ExpenseViewModel.FindAsync(id);

            if (expenseviewmodel != null)
            {
                ExpenseViewModel = expenseviewmodel;
                _context.ExpenseViewModel.Remove(ExpenseViewModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
