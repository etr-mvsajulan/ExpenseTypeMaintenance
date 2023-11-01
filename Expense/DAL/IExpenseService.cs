using System;
using System.Collections.Generic;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense.DAL
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseViewModel> GetExpenseList(string search = "", int page = 1, int currentPage = 1, int itemsPerPage = 10);
        ExpenseViewModel GetExpenseByID(int id, bool isShow = true);
        void CreateExpenseAsync(CreateExpenseViewModel expense);
        void UpdateExpenseAsync(UpdateExpenseViewModel expense);
        void DeleteExpenseAsync(int id);
        string GenerateCode();
        IEnumerable<ExpenseDetailsViewModel> GetExpenseDetailsList(int expenseid, bool isShow = true);
    }
}
