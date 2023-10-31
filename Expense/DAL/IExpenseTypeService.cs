using System;
using System.Collections.Generic;
using Expense.Models;

namespace Expense.DAL
{
    public interface IExpenseTypeService
    {
        IEnumerable<ExpensetypeViewModel> GetExpensetypes(string search = "", int page = 1, int currentPage = 1, int itemsPerPage = 10);
        ExpensetypeViewModel GetExpenseTypeById(int ID);
        void CreateExpenseType(CreateExpenseTypeModel expenseType);
        void DeleteExpenseType(int ID);
        void UpdateExpenseType(UpdateExpenseTypeModel expensetype);
        string GenerateCode();  
    }
}
