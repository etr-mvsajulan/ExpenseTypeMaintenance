using System;
using System.Collections.Generic;
using Expense.Models;
namespace Expense.DAL
{
    public interface IExpenseTypeService
    {
        IEnumerable<ExpensetypeViewModel> GetExpensetypes(string search);
        ExpensetypeViewModel GetExpenseTypeById(int ID);
        void CreateExpenseType(ExpensetypeViewModel expenseType);
        void DeleteExpenseType(int ID);
        void UpdateExpenseType(ExpensetypeViewModel expensetype);
    }
}
