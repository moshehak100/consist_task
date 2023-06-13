using consist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consist.Services
{
    public class DemoUsersValidatorService : IUsersValidator
    {
        public Task<bool> Validate(string user, string password)
        {
            bool isValid = password == "123";

            return Task.FromResult(isValid);
        }
    }
}
