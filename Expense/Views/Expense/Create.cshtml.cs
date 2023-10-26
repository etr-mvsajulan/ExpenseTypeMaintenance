using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Expense.DAL;
using Expense.Models;

namespace Expense.Views.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly Expense.DAL.ExpenseDBContext _context;

        public CreateModel(Expense.DAL.ExpenseDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExpenseViewModel ExpenseViewModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ExpenseViewModel == null || ExpenseViewModel == null)
            {
                return Page();
            }

            _context.ExpenseViewModel.Add(ExpenseViewModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
