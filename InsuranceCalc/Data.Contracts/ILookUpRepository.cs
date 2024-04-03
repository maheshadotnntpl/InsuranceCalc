using InsuranceCalc.Utility;

namespace InsuranceCalc.Data.Contracts
{
    public interface ILookUpRepository
    {
        ExecutionStatus GetOccupations();
    }
}
