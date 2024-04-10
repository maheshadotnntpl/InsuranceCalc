using InsuranceCalc.Models.Authenticate;
using InsuranceCalc.Utility;

namespace InsuranceCalc.Data.Contracts
{
    public interface IAccountRepository
    {
        Task<ExecutionStatus> Login(LoginModel model);
        Task<ExecutionStatus> Register(RegisterModel model);
        Task<ExecutionStatus> RegisterAdmin(RegisterModel model);
    }
}
