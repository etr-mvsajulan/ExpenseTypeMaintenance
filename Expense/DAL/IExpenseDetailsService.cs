using Expense.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Expense.DAL
{
    public interface IExpenseDetailsService
    {
        IEnumerable<ExpenseDetailsViewModel> GetExpenseDetails();
        ExpenseDetailsViewModel GetDetailsByID (int id);
        void CreateDetails(CreateExpenseDetailsViewModel details);
        void UpdateDetails(UpdateExpenseDetailsViewModel details);
        void DeleteDetails(int id);
        List<ExpensetypeViewModel> GetET();
        string GetDescriptionbByEID (int id);
    }
}
