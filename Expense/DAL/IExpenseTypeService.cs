using System;
using System.Collections.Generic;
using Expense.Models;
namespace Expense.DAL
{
    public interface IExpenseTypeService
    {
        IEnumerable<ExpensetypeViewModel> GetExpensetypes(string search, int page, int currentPage, int itemsPerPage);
        ExpensetypeViewModel GetExpenseTypeById(int ID);
        void CreateExpenseType(CreateExpenseTypeModel expenseType);
        void DeleteExpenseType(int ID);
        void UpdateExpenseType(UpdateExpenseTypeModel expensetype);
        string GenerateCode();     
    }
}
