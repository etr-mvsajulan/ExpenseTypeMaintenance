using System;
using System.Collections.Generic;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense.DAL
{
    public interface IExpenseService
    {
        Task <IEnumerable<ExpenseViewModel>> GetExpenseList(string search = "", int page = 1, int currentPage = 1, int itemsPerPage = 10);
        Task <ExpenseViewModel> GetExpenseByID(int id);
        Task<int> CreateExpenseAsync(CreateExpenseViewModel expense);
        Task UpdateExpenseAsync(UpdateExpenseViewModel expense);
        Task DeleteExpenseAsync(int id);
        Task<string> GenerateCode();
        Task <IEnumerable<ExpenseDetailsViewModel>> GetExpenseDetailsList(int expenseid);
    }
}
