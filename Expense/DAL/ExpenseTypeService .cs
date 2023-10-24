using System;
using System.Collections.Generic;
using System.Linq;
using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;

namespace Expense.DAL
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly ExpenseTypeDBContext _context;

        public ExpenseTypeService(ExpenseTypeDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExpensetypeViewModel> GetExpensetypes(string search)
        {
            var expenseTypes = _context.ExpenseTypes.Where(x=> string.IsNullOrEmpty(search) || x.Description.Contains(search)).Select(x => new ExpensetypeViewModel
            {
                ExpenseTypeID = x.ExpenseTypeID,
                Code = x.Code,
                Description = x.Description
            }).ToList();


            return expenseTypes;
        }

        public ExpensetypeViewModel GetExpenseTypeById(int ID)
        {
            var expenseType = _context.ExpenseTypes.Where(x => x.ExpenseTypeID == ID).Select(x => new ExpensetypeViewModel
            {
                ExpenseTypeID = x.ExpenseTypeID,
                Code = x.Code,
                Description = x.Description
            }).FirstOrDefault();

            return expenseType;
        }

        public void CreateExpenseType(ExpensetypeViewModel expensetype)
        {
            var newExpenseType = new ExpenseMaintenance
            {
                Code = expensetype.Code,
                Description = expensetype.Description
            };
        }

        public void UpdateExpenseType(ExpensetypeViewModel expenseType)
        {
            var existingExpenseType = _context.ExpenseTypes.Find(expenseType.ExpenseTypeID);
            if (existingExpenseType != null)
            {
                existingExpenseType.Code = expenseType.Code;
                existingExpenseType.Description = expenseType.Description;
                _context.SaveChanges();
            }
            
        }

        public void DeleteExpenseType(int ID)
        {
            var deleteExpenseType = _context.ExpenseTypes.Find(ID);
            if (deleteExpenseType != null)
            {
                _context.ExpenseTypes.Remove(deleteExpenseType);
                _context.SaveChanges();
            }
        }


        public ExpensetypeViewModel GetExpenseTypeById(int? ID)
        {
            throw new NotImplementedException();
        }
    }
}
