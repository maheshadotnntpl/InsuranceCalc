using InsuranceCalc.Models;
using InsuranceCalc.Utility;

namespace InsuranceCalc.Data.Contracts
{
    public interface ICalcPremiumRepository
    {
        ExecutionStatus GetPremium(Customer customer);
    }
}
