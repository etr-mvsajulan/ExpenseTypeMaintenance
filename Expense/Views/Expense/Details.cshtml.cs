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
    public class DetailsModel : PageModel
    {
        private readonly Expense.DAL.ExpenseDBContext _context;

        public DetailsModel(Expense.DAL.ExpenseDBContext context)
        {
            _context = context;
        }

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
    }
}
