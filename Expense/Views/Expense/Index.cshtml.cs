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
    public class IndexModel : PageModel
    {
        private readonly Expense.DAL.ExpenseDBContext _context;

        public IndexModel(Expense.DAL.ExpenseDBContext context)
        {
            _context = context;
        }

        public IList<ExpenseViewModel> ExpenseViewModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExpenseViewModel != null)
            {
                ExpenseViewModel = await _context.ExpenseViewModel.ToListAsync();
            }
        }
    }
}
