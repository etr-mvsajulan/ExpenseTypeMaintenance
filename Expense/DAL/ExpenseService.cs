using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Expense.DAL
{
    public class ExpenseService /*: IExpenseService*/
    {
        private readonly ExpenseDBContext _context;

        public ExpenseService(ExpenseDBContext context)
        {
            this._context = context;
        }

        //public async Task<List<string>> GetExpenseAsync()
        //{
        //    // Implement your asynchronous data retrieval logic here
        //    return null;
        //}

        //public async Task<string> GetExpenseAsync(int id)
        //{
        //    // Implement your asynchronous data retrieval logic here
        //    return null;
        //}

        //public async Task CreateExpenseAsync(string data)
        //{
        //    // Implement your asynchronous item creation logic here
        //    return null;
        //}
        
        //public async Task UpdateExpenseAsync(int id, string data)
        //{
        //    // Implement your asynchronous item update logic here
        //    return null;
        //}

        //public async Task DeleteExpenseAsync(int id)
        //{
        //    // Implement your asynchronous item deletion logic here
        //    return null;
        //}
    }
}
