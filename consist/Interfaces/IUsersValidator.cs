using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consist.Interfaces
{
    public interface IUsersValidator
    {
        Task<bool> Validate(string user, string password);
    }
}
