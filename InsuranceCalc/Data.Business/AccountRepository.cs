using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models.Authenticate;
using InsuranceCalc.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace InsuranceCalc.Data.Business
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AccountRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ExecutionStatus> Login(LoginModel model)
        {
            _logger.Info("AccountRepository: Login method called");
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                _logger.Info("AccountRepository: Login method executed");
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "User Logged in successfully!",
                    Object = (new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    })

                };
            }
            return new ExecutionStatus()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Failed!",
                Object = ""

            };
        }

        public async Task<ExecutionStatus> Register(RegisterModel model)
        {
            _logger.Info("AccountRepository: Register method called");
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)

                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User already exists!",
                    Object = ""

                };

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User creation failed! Please check user details and try again.",
                    Object = result

                };
            _logger.Info("AccountRepository: Register method executed");
            return new ExecutionStatus()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User created successfully!",
                Object = ""

            };
        }

        public async Task<ExecutionStatus> RegisterAdmin(RegisterModel model)
        {
            _logger.Info("AccountRepository: RegisterAdmin method called");
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)

                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User already exists!",
                    Object = ""
                };

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)


                return new ExecutionStatus()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User creation failed! Please check user details and try again.",
                    Object = ""
                };

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            _logger.Info("AccountRepository: RegisterAdmin method executed");
            return new ExecutionStatus()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User created successfully!",
                Object = ""

            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
