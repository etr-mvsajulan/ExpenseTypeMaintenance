using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense.DAL;
using Expense.Models;

namespace Expense.Views.Expenses
{
    public class EditModel : PageModel
    {
        private readonly Expense.DAL.ExpenseDBContext _context;

        public EditModel(Expense.DAL.ExpenseDBContext context)
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

            var expenseviewmodel =  await _context.ExpenseViewModel.FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expenseviewmodel == null)
            {
                return NotFound();
            }
            ExpenseViewModel = expenseviewmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ExpenseViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseViewModelExists(ExpenseViewModel.ExpenseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExpenseViewModelExists(int id)
        {
          return (_context.ExpenseViewModel?.Any(e => e.ExpenseId == id)).GetValueOrDefault();
        }
    }
}
