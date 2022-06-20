using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using EventPlanning.DA.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IOptionsMonitor<AppMessages> _optionsMonitor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CaseController(
            ICaseService caseService, 
            IOptionsMonitor<AppMessages> optionsMonitor,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _caseService = caseService;
            _optionsMonitor = optionsMonitor;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAsync([FromBody] CaseCreateDTO caseDTO)
        {
            var userId = int.TryParse(_httpContextAccessor.HttpContext.User.GetSubjectId(), out var value) ? value : default;
            await _caseService.AddAsync(caseDTO, userId);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CaseShortDTO>>> GetAllAsync()
        {
            var result = await _caseService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaseView>> GetByIdAsync([FromRoute] int id)
        {
            if(await _caseService.ExistsAsync(id))
            {
                var result = await _caseService.GetByIdAsync(id);
                return Ok(result);
            }
            return BadRequest(_optionsMonitor.CurrentValue.CaseNotExist);            
        }
    }
}
