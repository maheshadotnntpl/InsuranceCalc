using InsuranceCalc.Contracts;
using InsuranceCalc.Models;

namespace InsuranceCalc.Business
{
    public class WhiteCollarCustomerManager : ICustomerManager
    {
        public double GetOccupationFactor() {
            return OccupationRatingConstants.WhiteCollar;
        }
    }
}
