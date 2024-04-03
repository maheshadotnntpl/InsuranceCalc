using InsuranceCalc.Business;
using InsuranceCalc.Contracts;
using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models;
using InsuranceCalc.Utility;
using NLog;
using System.Net;

namespace InsuranceCalc.Data.Business
{
    public class CalcPremiumRepository : ICalcPremiumRepository
    {
        private readonly CustomerManagerFactory _customerManagerFactory;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public CalcPremiumRepository(CustomerManagerFactory customerManagerFactory)
        {
            _customerManagerFactory = customerManagerFactory;

        }
        public ExecutionStatus GetPremium(Customer customer)
        {
            try
            {
                _logger.Info("CalcPremiumRepository: GetPremium method called");

                ICustomerManager customerManager = _customerManagerFactory.GetCustomerManager(customer.Occupation);
                var premium = (customer.SumAssured * customerManager.GetOccupationFactor() * customer.Age) / (1000 * 12);

                _logger.Info("CalcPremiumRepository: GetPremium method executed");
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.OK,
                    Object = premium
                };
            }
            catch (Exception ex)
            {
                _logger.Info($"CalcPremiumRepository: Exception Occured:  {ex.Message}");
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Object = "",
                    Message = $"Issue while GetPremium {ex.Message}"
                };
            }
        }

    }
}
