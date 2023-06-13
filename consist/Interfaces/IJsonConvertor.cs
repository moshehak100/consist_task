using consist.DataTypes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace consist.Interfaces
{
    public interface IJsonConvertor
    {
        List<FamilyPerson> JsonListToObjects(string json);
    }
}
