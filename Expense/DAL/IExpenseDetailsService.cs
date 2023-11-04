using Expense.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Expense.DAL
{
    public interface IExpenseDetailsService
    {
        Task <IEnumerable<ExpenseDetailsViewModel>> GetExpenseDetails();
        Task <ExpenseDetailsViewModel> GetDetailsByID (int id);
        Task CreateDetails(CreateExpenseDetailsViewModel details);
        Task UpdateDetails(UpdateExpenseDetailsViewModel details);
        Task DeleteDetails(int id);
        Task<List<ExpensetypeViewModel>> GetET();
        Task<string> GetDescriptionbByEID (int id);
    }
}
