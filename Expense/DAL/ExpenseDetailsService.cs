using PagedList;
using PagedList.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Expense.Models.DBEntities;
using System.Security.Cryptography;


namespace Expense.DAL
{
    public class ExpenseDetailsService : IExpenseDetailsService
    {
        private readonly ExpenseDBContext _context;
        private readonly IVatComputationService _vatComputationService;
        private readonly INetOfVatComputationService _NetOfVatComputationService;
        private readonly IExpenseService _ExpenseService;

        public ExpenseDetailsService(ExpenseDBContext context, IVatComputationService vatComputationService, INetOfVatComputationService iNetOfVatComputationService, IExpenseService expenseService)
        {
            this._context = context;
            this._vatComputationService = vatComputationService;
            this._NetOfVatComputationService = iNetOfVatComputationService;
            this._ExpenseService = expenseService;
        }

        public async Task<IEnumerable<ExpenseDetailsViewModel>> GetExpenseDetails()
        {
            var detailsList = await _context.ExpenseDetails.Select(x => new ExpenseDetailsViewModel
            {
                ExpenseDetailID = x.ExpenseDetailID,
                Expenseid = x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID,
                Amount= x.Amount,
                Remarks= x.Remarks,
                NetOfVatAmount= x.NetOfVatAmount,
                VatAmount= x.VatAmount
            }).ToListAsync();

            return detailsList;
        }

        public async Task<ExpenseDetailsViewModel> GetDetailsByID(int id)
        {
            var detailsList = await _context.ExpenseDetails.Where(x=> x.ExpenseDetailID == id).Select(x=> new ExpenseDetailsViewModel
            {
                ExpenseDetailID= x.ExpenseDetailID,
                Expenseid= x.Expenseid,
                ExpenseTypeID = x.ExpenseTypeID ,
                Amount= x.Amount,
                Remarks= x.Remarks,
                NetOfVatAmount = x.NetOfVatAmount,
                VatAmount= x.VatAmount
            }).FirstOrDefaultAsync();

            return detailsList;
        }

        public async Task CreateDetails(CreateExpenseDetailsViewModel details)
        {
            var newDetails = new ExpenseDetails()
            {
                Expenseid = details.Expenseid,
                ExpenseTypeID= details.ExpenseTypeID,
                Amount= details.Amount,
                Remarks= details.Remarks,
                NetOfVatAmount = (decimal)_NetOfVatComputationService.ComputeNetOfVat(Convert.ToDouble(details.Amount)),
                VatAmount= (decimal)_vatComputationService.ComputeVat(Convert.ToDouble(details.Amount)),
            };

            _context.ExpenseDetails.Add(newDetails);
            await _context.SaveChangesAsync();

            await UpdateHeader(details.Expenseid);
        }

        public async Task UpdateDetails(UpdateExpenseDetailsViewModel details)
        {
            var updateDetails = _context.ExpenseDetails.Find(details.ExpenseDetailID);
            if (updateDetails != null)
            {
                updateDetails.ExpenseTypeID = details.ExpenseTypeID;
                updateDetails.Amount = details.Amount;
                updateDetails.Remarks = details.Remarks;
                updateDetails.NetOfVatAmount= (decimal)_NetOfVatComputationService.ComputeNetOfVat(Convert.ToDouble(details.Amount));
                updateDetails.VatAmount = (decimal)_vatComputationService.ComputeVat(Convert.ToDouble(details.Amount));

                await _context.SaveChangesAsync();

                await UpdateHeader(updateDetails.Expenseid);
            }
        }

        public async Task DeleteDetails (int id)
        {
            var deleteDetails = _context.ExpenseDetails.Find(id);
            if (deleteDetails != null)
            {
                _context.ExpenseDetails.Remove(deleteDetails);
                await _context.SaveChangesAsync();
                await UpdateHeader(deleteDetails.Expenseid);
            }
        }

        public async Task<List<ExpensetypeViewModel>> GetET()
        {
            var expenseTypes = await _context.ExpenseTypes.Select(x => new ExpensetypeViewModel
            {
                Code = x.ExpenseTypeID.ToString(),
                Description = x.Code + " - " + x.Description,
            }).ToListAsync();

            return expenseTypes;
        }

        public async Task<string> GetDescriptionbByEID(int id)
        {
            var description = await _context.ExpenseTypes.Where(x=> x.ExpenseTypeID == id).FirstOrDefaultAsync();
            if (description != null)
            {
                return description.Code + description.Description;
            }

            return "Expense Not Found";
            
        }

        public async Task UpdateHeader(int id)
        {
            var details = await _ExpenseService.GetExpenseDetailsList(id);
            decimal totalTaxableAmount = details
                   .Sum(ed => ed.Amount);

            decimal totalNetOfVatAmount = details
                .Sum(ed => ed.NetOfVatAmount);

            // Calculate the sum of VatAmount in ExpenseDetails
            decimal totalVatAmount = details
                .Sum(ed => ed.VatAmount);

            var header = _context.Expense.Find(id);
            if (header != null)
            {
                header.TaxableTotal = totalTaxableAmount;
                header.NetOfVatTotal = totalNetOfVatAmount;
                header.VatTotal = totalVatAmount;
            }
            await _context.SaveChangesAsync();
        }
    }
}
