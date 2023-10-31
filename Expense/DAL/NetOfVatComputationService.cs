namespace Expense.DAL
{
    public class NetOfVatComputationService : INetOfVatComputationService
    {

        private readonly IVatComputationService _vatServices;
        public NetOfVatComputationService (IVatComputationService vatServices)
        {
            _vatServices = vatServices;
        }

        public double ComputeNetOfVat(double Amount)
        {
            double vatAmount = _vatServices.ComputeVat(Amount);
            double netAmount = Amount - vatAmount;

            return netAmount;
        }
    }
}
