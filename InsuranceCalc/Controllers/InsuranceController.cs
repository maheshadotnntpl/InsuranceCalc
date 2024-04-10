using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models;
using InsuranceCalc.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace InsuranceCalc.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]   
    public class InsuranceController : ControllerBase
    {
        private readonly ICalcPremiumRepository _calcPremiumRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public InsuranceController(ICalcPremiumRepository calcPremiumRepository) {
            _calcPremiumRepository = calcPremiumRepository;
        }

        [HttpPost("CalcPremium")]
        public ExecutionStatus CalcPremium(Customer customer)
        {
            _logger.Info("InsuranceController: CalcPremium method called");
            var outPut = _calcPremiumRepository.GetPremium(customer);
            _logger.Info("InsuranceController: CalcPremium method executed");
            return outPut;
        }
    }
}
