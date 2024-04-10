using Azure;
using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models.Authenticate;
using InsuranceCalc.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace InsuranceCalc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Login")]
        public async Task<ExecutionStatus> Login([FromBody] LoginModel model)
        {
            _logger.Info("AccountController: Login method called");
            var result = await _accountRepository.Login(model);
            _logger.Info("AccountController: Login method executed");
            return result;
        }

        [HttpPost("register")]
        public async Task<ExecutionStatus> Register([FromBody] RegisterModel model)
        {
            _logger.Info("AccountController: Register method called");
            var result = await _accountRepository.Register(model);
            _logger.Info("AccountController: Register method executed");
            return result;
        }

        [HttpPost("register-admin")]
        public async Task<ExecutionStatus> RegisterAdmin([FromBody] RegisterModel model)
        {
            _logger.Info("AccountController: RegisterAdmin method called");
            var result = await _accountRepository.RegisterAdmin(model);
            _logger.Info("AccountController: RegisterAdmin method executed");
            return result;
        }
    }
}
