using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyHomeServer.Server.Data.DbModels;
using MyHomeServer.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyHomeServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            string userName = user.UserName;
            string password = user.Password;
            string email = user.EmailAdress;

            ApplicationUser identityUser = new ApplicationUser()
            {
                UserName = userName,
                Email = email
            };
            IdentityResult userIdentityResult = await _userManager.CreateAsync(identityUser, password);
            IdentityResult roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Administrator");

            if (userIdentityResult.Succeeded && roleIdentityResult.Succeeded == true)
            {
                return Ok(new { userIdentityResult.Succeeded });
            }
            else
            {
                string errorsToReturn = "Реєстрація не виконана. Перевірте помилки нижче.";
                foreach (var error in userIdentityResult.Errors)
                {
                    errorsToReturn += Environment.NewLine;
                    errorsToReturn += $"Помилки: {error.Code} - {error.Description}";
                }
                return StatusCode(StatusCodes.Status500InternalServerError, errorsToReturn);
            }
        }
        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserDTO user)
        {
            string userName = user.UserName;
            string password = user.Password;

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            if (signInResult.Succeeded == true)
            {
                ApplicationUser identityUser = await _userManager.FindByNameAsync(userName);
                string JsonWebTokensAsString = await GenerateJSONWebToken(identityUser);
                return Ok(JsonWebTokensAsString);
            }
            else
            {
                return Unauthorized(user);
            }
        }
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<string> GenerateJSONWebToken(ApplicationUser identityUser)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Actort, identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.UtcNow.AddDays(28),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
