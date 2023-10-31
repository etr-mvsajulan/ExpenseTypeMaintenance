using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;
using NuGet.Protocol.Core.Types;

namespace Expense.Controllers
{
    public class ExpenseDetailsController : Controller
    {
        private readonly IExpenseDetailsService _detailService;

        public ExpenseDetailsController(IExpenseDetailsService detailService)
        {
            _detailService = detailService;
        }


        // GET: ExpenseDetails
        public async Task<IActionResult> Index()
        {
            var expenseDetails = await Task.Run(() => _detailService.GetExpenseDetails());
            return View(expenseDetails);
        }

        // GET: ExpenseDetails/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var details = await Task.Run(() => _detailService.GetDetailsByID(id));
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: ExpenseDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExpenseDetailsViewModel details)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _detailService.CreateDetails(details));
                return RedirectToAction("Edit", "Expenses", new { id = details.Expenseid }); ;
            }
            return View(details);
        }

        // GET: ExpenseDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var details = await Task.Run(() => _detailService.GetDetailsByID(id));
            if (details == null)
            {
                return NotFound();
            }

           
            return View(details);
        }

        // POST: ExpenseDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateExpenseDetailsViewModel details)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _detailService.UpdateDetails(details));
                    return RedirectToAction("Edit", "Expenses", new { id = details.Expenseid });
                }

                catch (Exception ex)
                {
                    return View(ex.Message);
                }              
            }
            else
            {
                return View();
            }
        }

        // GET: ExpenseDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var detailsList = await Task.Run(() => _detailService.GetDetailsByID(id));
                return detailsList != null ? View(detailsList) : NotFound();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // POST: ExpenseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ExpenseDetailsViewModel details)
        {
            try
            {
                if (details != null)
                {
                    int EID = await Task.Run(() => _detailService.GetDetailsByID(details.ExpenseDetailID).Expenseid);
                    await Task.Run(() => _detailService.DeleteDetails(details.ExpenseDetailID));
                    return RedirectToAction("Edit", "Expenses", new { id = EID });
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        [HttpGet]
        public JsonResult LoadExpensetypes()
        {
            var expenseTypes = _detailService.GetET();
            return new JsonResult(expenseTypes);
        }

        private string GetExpenseTypeDescription(int expenseTypeID)
        {
            // Implement code to fetch the description for the given expenseTypeID from your data source
            // For example, you can query your database or use a service to get the description.
            // Replace the following line with your actual logic.
            var description = _detailService.GetDescriptionbByEID(expenseTypeID);

            return description;
        }
    }
}
