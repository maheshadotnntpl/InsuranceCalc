using InsuranceCalc.Contracts;
using InsuranceCalc.Models;

namespace InsuranceCalc.Business
{
    public class HeavyManualCustomerManager : ICustomerManager
    {
        public double GetOccupationFactor()
        {
            return OccupationRatingConstants.HeavyManual;
        }
    }
}
