using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Expense.Models.DBEntities;

namespace Expense.DAL
{
    public class ExpenseDetailsService : IExpenseDetailsService
    {

        private readonly ExpenseDBContext _context;
        public ExpenseDetailsService(ExpenseDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExpenseDetailsViewModel> GetExpenseDetails()
        {
            var detailsList = _context.ExpenseDetails.Select(x => new ExpenseDetailsViewModel
            {
                ExpenseDetailID = x.ExpenseDetailID,
                Expenseid = x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID,
                Amount= x.Amount,
                Remarks= x.Remarks,
                NetOfVatAmount= x.NetOfVatAmount,
                VatAmount= x.VatAmount
            }).ToList();

            return detailsList;
        }

        public ExpenseDetailsViewModel GetDetailsByID(int id)
        {
            var detailsList = _context.ExpenseDetails.Where(x=> x.ExpenseDetailID == id).Select(x=> new ExpenseDetailsViewModel
            {
                ExpenseDetailID= x.ExpenseDetailID,
                Expenseid= x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID ,
                Amount= x.Amount,
                Remarks= x.Remarks,
                NetOfVatAmount = x.NetOfVatAmount,
                VatAmount= x.VatAmount
            }).FirstOrDefault();

            return detailsList;
        }

        public void CreateDetails(CreateExpenseDetailsViewModel details)
        {
            var newDetails = new ExpenseDetails()
            {
                Expenseid = details.Expenseid,
                ExpenseTypeID= details.ExpenseTypeID,
                Amount= details.Amount,
                Remarks= details.Remarks,
                NetOfVatAmount = details.NetOfVatAmount,
                VatAmount= details.VatAmount,
            };

            _context.ExpenseDetails.Add(newDetails);
            _context.SaveChanges();
        }

        public void UpdateDetails(UpdateExpenseDetailsViewModel details)
        {
            var updateDetails = _context.ExpenseDetails.Find(details.ExpenseDetailID);
            if (updateDetails != null)
            {
                updateDetails.ExpenseTypeID = details.ExpenseTypeID;
                updateDetails.Amount = details.Amount;
                updateDetails.Remarks = details.Remarks;
                updateDetails.NetOfVatAmount= details.NetOfVatAmount;
                updateDetails.VatAmount = details.VatAmount;
                _context.SaveChanges();
            }
        }

        public void DeleteDetails (int id)
        {
            var deleteDetails = _context.ExpenseDetails.Find(id);
            if (deleteDetails != null)
            {
                _context.ExpenseDetails.Remove(deleteDetails);
                _context.SaveChanges();
            }
        }

        public List<ExpensetypeViewModel> GetET()
        {
            var expenseTypes = _context.ExpenseTypes.Select(x => new ExpensetypeViewModel
            {
                Code = x.ExpenseTypeID.ToString(),
                Description = x.Code + " - " + x.Description,
            }).ToList();

            return expenseTypes;
        }

        public string GetDescriptionbByEID(int id)
        {
            var description = _context.ExpenseTypes.Where(x=> x.ExpenseTypeID == id).FirstOrDefault();
            if (description != null)
            {
                return description.Code + description.Description;
            }

            return "Expense Not Found";
            
        }


    }
}
