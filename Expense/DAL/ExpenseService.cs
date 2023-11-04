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

        public async Task <IEnumerable<ExpenseViewModel>> GetExpenseList(string search, int page = 1, int currentPage = 1, int itemsPerPage = 5)
        {
            var expenseList = await _context.Expense.Where(x=> string.IsNullOrEmpty(search) || x.TransactionNumber.Contains(search)).Select(x => new ExpenseViewModel
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
            }).OrderBy(x=> x.TransactionNumber).ToListAsync();

            return expenseList;
        }

        public async Task<ExpenseViewModel> GetExpenseByID(int id)
        {
            var Expense = await _context.Expense.Where(x=> x.ExpenseId == id).Select(x=> new ExpenseViewModel
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
                
            }).FirstOrDefaultAsync();

            Expense.ExpenseDetails = await GetExpenseDetailsList(id);
            return Expense;
        }

        public async Task<int> CreateExpenseAsync (CreateExpenseViewModel expense)
        {
            var newExpense = new Expense.Models.DBEntities.Expense
            {
                TransactionNumber = await GenerateCode(),
                TransactionDate = expense.TransactionDate,
                CostUnitCode = expense.CostUnitCode,
                CostUnitName = expense.CostUnitName,
                Remarks = expense.Remarks,
                TaxableTotal = 0,
                VatTotal = 0,
                NetOfVatTotal = 0,
                Status = (int)(CreateExpenseViewModel.ExpenseStatus)expense.Status,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                UpdatedBy = 1,
                UpdatedDate = DateTime.Now
            };

            _context.Expense.Add(newExpense);
            await _context.SaveChangesAsync();
            return newExpense.ExpenseId;
        }

        public async Task UpdateExpenseAsync(UpdateExpenseViewModel expense)
        {
            var updateExpense = _context.Expense.Find(expense.ExpenseId);
            if (updateExpense != null)
            {
                updateExpense.TransactionDate = expense.TransactionDate;
                updateExpense.CostUnitCode = expense.CostUnitCode;
                updateExpense.CostUnitName = expense.CostUnitName;
                updateExpense.Remarks = expense.Remarks;
                updateExpense.Status = (int)(UpdateExpenseViewModel.ExpenseStatus)expense.Status;
                updateExpense.UpdatedBy = expense.UpdatedBy;
                updateExpense.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var deleteExpense = _context.Expense.Find(id);
            var deleteDetails = _context.ExpenseDetails.Where(x => x.Expenseid == id);
            if (deleteExpense != null)
            {
                _context.Expense.Remove(deleteExpense);
                _context.ExpenseDetails.RemoveRange(deleteDetails);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateCode()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            string sql = "SELECT @result = (NEXT VALUE FOR ExpenseTransactions)";

            await Task.Run(async () => {
                await _context.Database.ExecuteSqlRawAsync(sql, result);
            });

            int values = (int)result.Value;
            string NextSequence = values.ToString("D10");
            return NextSequence;
        }

        public async Task<IEnumerable<ExpenseDetailsViewModel>> GetExpenseDetailsList(int expenseid)
        {
            var expenseDetails = await _context.ExpenseDetails.Select(x => new ExpenseDetailsViewModel
            {
                ExpenseDetailID = x.ExpenseDetailID,
                Expenseid = x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID,
                Description = _context.ExpenseTypes.Where(y=> y.ExpenseTypeID == x.ExpenseTypeID).Select(y=> y.Code + " - " + y.Description).FirstOrDefault(),
                Amount = x.Amount,
                Remarks = x.Remarks,
                NetOfVatAmount = x.NetOfVatAmount,
                VatAmount = x.VatAmount

            }).Where(x=> x.Expenseid == expenseid).ToListAsync();

            return expenseDetails;
        }

       
    }
}
