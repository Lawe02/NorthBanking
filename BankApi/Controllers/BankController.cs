using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using WebbApp.Data;
using WebbApp.Models;
using Microsoft.Extensions.Configuration;

namespace BankApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly BankAppDataContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context,BankAppDataContext dbContext, IConfiguration configuraion)
        {
            _context = context;
            _dbContext = dbContext;
            _configuration = configuraion;
        }
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [HttpPost("login")]
        public IActionResult Login(LoginRequestModel model)
        { 
            var user = _context.Users.FirstOrDefault(u => u.UserName == model.Username);
            if (user == null)
                return Unauthorized();

            var passwordHasher = new PasswordHasher<LoginRequestModel>();

            var result = passwordHasher.VerifyHashedPassword(model, user.   PasswordHash, model.Password);

            if(result == PasswordVerificationResult.Success)
            {
                var token = GenerateJwtToken(user.UserName, "Cashier");
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
               
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankAppDataContext _dbContext;

        public BankController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("me/{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);

            // Handle the response
            if (customer != null)
            {
                // Process the successful response
                return Ok(customer);
            }
            else
            {
                // Handle other error cases
                return StatusCode(404);
            }
        }
     
        [HttpGet("account/{id}/{limit}/{offset}")]
        public IActionResult GetAccountTransactions(int id, int limit, int offset)
        {
            var transactions = _dbContext.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .Skip(offset)
                .Take(limit)
                .ToList();

            if (transactions.Count == 0)
                return NotFound();

            return Ok(transactions);
        }
    }
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
