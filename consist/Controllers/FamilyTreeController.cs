using consist.DataTypes;
using consist.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace consist.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FamilyTreeController : Controller
    {
        private readonly ILogger<FamilyTreeController> _logger;
        private readonly IFamilyTreeService _familyTreeService;

        public FamilyTreeController(ILogger<FamilyTreeController> logger, IFamilyTreeService familyTreeService)
        {
            _logger = logger;
            _familyTreeService = familyTreeService;
        }

        [HttpGet]
        public async Task<IActionResult> ConvertPersonsListToFamilyTree(string familyList)
        {
            try
            {
                // ToDo: we should validate here the json, i assumed that the input is valid - not for real project

                List<FamilyPerson> familyTree = await _familyTreeService.ConvertPersonsListToFamilyTree(familyList);

                return Ok(familyTree);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ConvertPersonsListToFamilyTree failed {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
