using System;
namespace Expense.DAL
{
    public interface INetOfVatComputationService
    {
        double ComputeNetOfVat(double Amount);
    }
}
