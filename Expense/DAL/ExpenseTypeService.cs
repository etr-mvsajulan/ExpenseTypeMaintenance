using System;
using System.Collections.Generic;
using System.Linq;
using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace Expense.DAL
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly ExpenseTypeDBContext _context;

        public ExpenseTypeService(ExpenseTypeDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExpensetypeViewModel> GetExpensetypes(string search, int page = 1, int currentPage = 1, int itemsPerPage = 10)
        {
            var expenseTypes = _context.ExpenseTypes.Where(x => string.IsNullOrEmpty(search) || x.Description.Contains(search)).Select(x => new ExpensetypeViewModel
            {
                ExpenseTypeID = x.ExpenseTypeID,
                Code = x.Code,
                Description = x.Description
            }).ToList().OrderBy(x=> x.ExpenseTypeID);

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

        public void CreateExpenseType(CreateExpenseTypeModel expensetype)
        {
            var newExpenseType = new ExpenseTypes
            {
                Code = expensetype.Code,
                Description = expensetype.Description
                
            };              
            _context.ExpenseTypes.Add(newExpenseType);
            _context.SaveChanges();

        }

        public void UpdateExpenseType(UpdateExpenseTypeModel expenseType)
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

        public string GenerateCode()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR ExpenseSequence)", result);

            int values =  (int)result.Value;
            string NextSequence = values.ToString("D5");
            return NextSequence;
        }

        public ExpensetypeViewModel GetExpenseTypeById(int? ID)
        {
            throw new NotImplementedException();
        }

       
    }
}
