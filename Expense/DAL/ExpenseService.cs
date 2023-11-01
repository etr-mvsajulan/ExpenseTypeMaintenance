using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http.HttpResults;
using Expense.Models.DBEntities;

namespace Expense.DAL
{
    public class ExpenseService:IExpenseService
    {
        private readonly ExpenseDBContext _context;

        public ExpenseService(ExpenseDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExpenseViewModel> GetExpenseList(string search, int page = 1, int currentPage = 1, int itemsPerPage = 5)
        {
            var expenseList = _context.Expense.Where(x=> string.IsNullOrEmpty(search) || x.TransactionNumber.Contains(search)).Select(x => new ExpenseViewModel
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
                Status = (ExpenseViewModel.ExpenseStatus)x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate
            }).ToList();

            return expenseList;
        }

        public ExpenseViewModel GetExpenseByID(int id, bool isShow)
        {
            var Expense = _context.Expense.Where(x=> x.ExpenseId == id).Select(x=> new ExpenseViewModel
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
                Status = (ExpenseViewModel.ExpenseStatus)x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                ExpenseDetails = GetExpenseDetailsList(id, isShow)
            }).FirstOrDefault();

            return Expense;
        }

        public void CreateExpenseAsync (CreateExpenseViewModel expense)
        {
            var newExpense = new Expense.Models.DBEntities.Expense
            {
                TransactionNumber = expense.TransactionNumber,
                TransactionDate = expense.TransactionDate,
                CostUnitCode = expense.CostUnitCode,
                CostUnitName = expense.CostUnitName,
                Remarks = expense.Remarks,
                TaxableTotal = expense.TaxableTotal,
                VatTotal = expense.VatTotal,
                NetOfVatTotal = expense.NetOfVatTotal,
                Status = (int)(CreateExpenseViewModel.ExpenseStatus)expense.Status,
                CreatedBy = expense.CreatedBy,
                CreatedDate = expense.CreatedDate,
                UpdatedBy = expense.UpdatedBy,
                UpdatedDate = expense.UpdatedDate
            };

            _context.Expense.Add(newExpense);
            _context.SaveChanges();
        }

        public void UpdateExpenseAsync(UpdateExpenseViewModel expense)
        {
            var updateExpense = _context.Expense.Find(expense.ExpenseId);
            if (updateExpense != null)
            {
                updateExpense.TransactionDate = expense.TransactionDate;
                updateExpense.CostUnitCode = expense.CostUnitCode;
                updateExpense.CostUnitName = expense.CostUnitName;
                updateExpense.Remarks = expense.Remarks;
                updateExpense.TaxableTotal = expense.TaxableTotal;
                updateExpense.VatTotal = expense.VatTotal;
                updateExpense.NetOfVatTotal = expense.NetOfVatTotal;
                updateExpense.Status = (int)(UpdateExpenseViewModel.ExpenseStatus)expense.Status;
                updateExpense.UpdatedBy = expense.UpdatedBy;
                updateExpense.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteExpenseAsync(int id)
        {
            var deleteExpense = _context.Expense.Find(id);
            var deleteDetails = _context.ExpenseDetails.Where(x => x.Expenseid == id);
            if (deleteExpense != null)
            {
                _context.Expense.Remove(deleteExpense);
                _context.ExpenseDetails.RemoveRange(deleteDetails);
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
            string NextSequence = values.ToString("D10");
            return NextSequence;
        }

        public IEnumerable<ExpenseDetailsViewModel> GetExpenseDetailsList(int expenseid, bool isShow)
        {
            var expenseDetails = _context.ExpenseDetails.Select(x => new ExpenseDetailsViewModel
            {
                ExpenseDetailID = x.ExpenseDetailID,
                Expenseid = x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID,
                Description = _context.ExpenseTypes.Where(y=> y.ExpenseTypeID == x.ExpenseTypeID).Select(y=> y.Code + " - " + y.Description).FirstOrDefault(),
                Amount = x.Amount,
                Remarks = x.Remarks,
                NetOfVatAmount = x.NetOfVatAmount,
                VatAmount = x.VatAmount,
                isShow = isShow

            }).Where(x=> x.Expenseid == expenseid).ToList();

            return expenseDetails;
        }

       
    }
}
