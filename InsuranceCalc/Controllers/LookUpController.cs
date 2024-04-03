using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Utility;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace InsuranceCalc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        private readonly ILookUpRepository _iLookUpRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public LookUpController(ILookUpRepository iLookUpRepository)
        {
            _iLookUpRepository = iLookUpRepository;
        }
        [HttpGet("GetOccupations")]
        public ExecutionStatus GetOccupations()
        {
            _logger.Info("LookUpController: GetOccupations method called");
            return _iLookUpRepository.GetOccupations();
        }
    }
}
