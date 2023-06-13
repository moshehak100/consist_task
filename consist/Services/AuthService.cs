using consist.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace consist.Services
{
    public class AuthService : IAuth
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsersValidator _usersValidator;

        public AuthService(ILogger<AuthService> logger, IConfiguration configuration, IUsersValidator usersValidator)
        {
            _logger = logger;
            _configuration = configuration;
            _usersValidator = usersValidator;
        }

        public async Task<String> CreateToken(string username, string password)
        {
            if (!await _usersValidator.Validate(username, password))
                throw new UnauthorizedAccessException();

            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration["JWT:PrivateKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:ExpirationPeriodSec"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
