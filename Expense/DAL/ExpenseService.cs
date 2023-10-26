using System;
using System.Collections.Generic;
using System.Linq;
using Expense.DAL;
using Expense.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Expense.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Expense.DAL
{
    public class ExpenseService : IExpenseService
    {
        public readonly ExpenseDBContext _context;

        public ExpenseService(ExpenseDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExpenseViewModel> GetExpenses(string search, int page, int currentPage, int itemsPerPage)
        {
            var expenses = _context.ExpenseViewModel.Where(x => string.IsNullOrEmpty(search) || x.TransactionNumber.Contains(search)).Select(x => new ExpenseViewModel
            {
                ExpenseId = x.ExpenseId,
                TransactionNumber = x.TransactionNumber,
                TransactionDate = x.TransactionDate,
                CostUnitName = x.CostUnitName,
                NetOfVatTotal = x.NetOfVatTotal,
                StatusEnum = x.StatusEnum,
            }).ToList().OrderBy(x => x.TransactionNumber);

            return expenses;
        }

        public ExpenseViewModel GetExpenseById(int ID)
        {
            var expense = _context.ExpenseViewModel.Where(x => x.ExpenseId == ID).Select(x => new ExpenseViewModel
            {
                ExpenseId = x.ExpenseId,
                TransactionNumber = x.TransactionNumber,
                TransactionDate = x.TransactionDate,
                CostUnitCode = x.CostUnitCode,
                CostUnitName = x.CostUnitName,
                Remarks = x.Remarks,
                TaxableTotal = x.TaxableTotal,
                VatTotal = x.VatTotal,
                NetOfVatTotal = x.NetOfVatTotal,
                Status = x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
            }).FirstOrDefault();

            return expense;
        }

        public void CreateExpense(CreateExpenseModel expense)
        {
            var newExpense = new ExpenseViewModel
            {
                TransactionNumber = expense.TransactionNumber,
                TransactionDate = expense.TransactionDate,
                CostUnitCode = expense.CostUnitCode,
                CostUnitName = expense.CostUnitName,
                Remarks = expense.Remarks,
                TaxableTotal = expense.TaxableTotal,
                VatTotal = expense.VatTotal,
                NetOfVatTotal = expense.NetOfVatTotal,
                Status = expense.Status,
                CreatedBy = expense.CreatedBy,
                CreatedDate = expense.CreatedDate,
                UpdatedBy = expense.UpdatedBy,
                UpdatedDate = expense.UpdatedDate,

            };
            _context.ExpenseViewModel.Add(newExpense);
            _context.SaveChanges();

        }

        public void UpdateExpense(UpdateExpenseModel expense)
        {
            var existingExpense = _context.ExpenseViewModel.Find(expense.ExpenseId);
            if (existingExpense != null)
            {
                existingExpense.TransactionNumber = expense.TransactionNumber;
                existingExpense.TransactionDate = expense.TransactionDate;
                existingExpense.CostUnitCode = expense.CostUnitCode;
                existingExpense.CostUnitName = expense.CostUnitName;
                existingExpense.Remarks = expense.Remarks;
                existingExpense.TaxableTotal = expense.TaxableTotal;
                existingExpense.VatTotal = expense.VatTotal;
                existingExpense.NetOfVatTotal = expense.NetOfVatTotal;
                existingExpense.Status = expense.Status;
                existingExpense.CreatedBy = expense.CreatedBy;
                existingExpense.CreatedDate = expense.CreatedDate;
                existingExpense.UpdatedBy = expense.UpdatedBy;
                existingExpense.UpdatedDate = expense.UpdatedDate;
                _context.SaveChanges();
            }

        }
        
        public void DeleteExpense(int ID)
        {
            var deleteExpenseType = _context.ExpenseViewModel.Find(ID);
            if (deleteExpenseType != null)
            {
                _context.ExpenseViewModel.Remove(deleteExpenseType);
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
                       "SELECT @result = (NEXT VALUE FOR ExpenseTransactions)", result);

            int values = (int)result.Value;
            string NextSequence = values.ToString("D5");
            return NextSequence;
        }

        public ExpensetypeViewModel GetExpenseTypeById(int? ID)
        {
            throw new NotImplementedException();
        }
    }
}
