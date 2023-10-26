using System;
using System.Collections.Generic;
using Expense.Models;

namespace Expense.DAL
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseViewModel> GetExpenses(string search, int page, int currentPage, int itemsPerPage);
        ExpenseViewModel GetExpenseById(int ID);
        void CreateExpense(CreateExpenseModel expenseType);
        void DeleteExpense(int ID);
        void UpdateExpense(UpdateExpenseModel expensetype);
        string GenerateCode();
    }
}
