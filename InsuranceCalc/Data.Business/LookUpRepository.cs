using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models;
using InsuranceCalc.Utility;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace InsuranceCalc.Data.Business
{
    public class LookUpRepository : ILookUpRepository
    {
        private readonly InsuranceContext _insuranceContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public LookUpRepository(InsuranceContext insuranceContext)
        {
            _insuranceContext = insuranceContext;
        }
        [HttpGet]
        public ExecutionStatus GetOccupations()
        {
            try
            {
                _logger.Info("LookUpRepository: GetOccupations method called");

                var occupations = _insuranceContext.Occupations.Select(x => new { x.Id, x.OccupationName });

                _logger.Info("LookUpRepository: GetOccupations method executed");
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.OK,
                    Object = occupations
                };
            }
            catch (Exception ex)
            {
                _logger.Info($"LookUpRepository: Exception Occured:  {ex.Message}");
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Object = "",
                    Message = $"Issue while GetOccupation {ex.Message}"
                };
            }
        }
    }
}
