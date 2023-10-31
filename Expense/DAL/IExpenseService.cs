using System;
using System.Collections.Generic;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense.DAL
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseViewModel> GetExpenseList();
        ExpenseViewModel GetExpenseByID(int id);
        void CreateExpenseAsync(CreateExpenseViewModel expense);
        void UpdateExpenseAsync(UpdateExpenseViewModel expense);
        void DeleteExpenseAsync(int id);
        string GenerateCode();
        IEnumerable<ExpenseDetailsViewModel> GetExpenseDetailsList(int expenseid);
    }
}
