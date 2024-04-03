using InsuranceCalc.Contracts;
using InsuranceCalc.Models;

namespace InsuranceCalc.Business
{
    public class LightManualCustomerManager : ICustomerManager
    {
        public double GetOccupationFactor()
        {
            return OccupationRatingConstants.LightManual;
        }
    }
}
