using InsuranceCalc.Contracts;
using InsuranceCalc.Models;

namespace InsuranceCalc.Business
{
    public class CustomerManagerFactory
    {
        public ICustomerManager GetCustomerManager(OccupationConstants occupation)
        {
           
            ICustomerManager? returnValue = null;
            if (occupation == OccupationConstants.Cleaner || occupation == OccupationConstants.Florist)
            {
                returnValue = new LightManualCustomerManager();
            }
            else if (occupation == OccupationConstants.Farmer || occupation == OccupationConstants.Mechanic)
            {
                returnValue = new HeavyManualCustomerManager();
            }
            else if (occupation == OccupationConstants.Doctor)
            {
                returnValue = new ProfessionalCustomerManager();
            }
            else if(occupation == OccupationConstants.Author)
            {
                returnValue = new WhiteCollarCustomerManager();
            }
            else
            {
                throw new Exception("Enter the proper occupation");
            }
            return returnValue;
        }
    }
}
