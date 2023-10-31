using System;
namespace Expense.DAL
{
    public interface IVatComputationService
    {
        double ComputeVat(double Amount);

    }
}
