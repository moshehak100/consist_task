using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace consist.Interfaces
{
    public interface IAuth
    {
        /// <summary>
        /// This method validate user credentials and create the appropriate token for it
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        /// if user credentials valid it return the Token
        /// otherwise it throws UnauthorizedAccessException exception
        /// </returns>
        Task<String> CreateToken(string username, string password);
    }
}
