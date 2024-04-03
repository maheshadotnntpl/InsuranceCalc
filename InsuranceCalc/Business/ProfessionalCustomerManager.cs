using InsuranceCalc.Contracts;
using InsuranceCalc.Models;

namespace InsuranceCalc.Business
{
    public class ProfessionalCustomerManager : ICustomerManager
    {
        public double GetOccupationFactor() {
            return OccupationRatingConstants.Professional;
        }
    }
}
