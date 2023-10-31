namespace Expense.DAL
{
    public class VatComputationService : IVatComputationService
    {

        public double ComputeVat(double Amount) 
        {
            double vatAmount = (Amount * 0.19) + 25;

            return vatAmount;
        }
    }
}
