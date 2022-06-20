using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using EventPlanning.DA.Models;
using Microsoft.AspNetCore.Authorization;
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
        ISmsService _smsService;

        public CaseController(ICaseService caseService, IOptionsMonitor<AppMessages> optionsMonitor, ISmsService smsService)
        {
            _caseService = caseService;
            _optionsMonitor = optionsMonitor;
            _smsService = smsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAsync([FromBody] CaseCreateDTO caseDTO)
        {
            await _caseService.AddAsync(caseDTO);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CaseShortDTO>>> GetAllAsync()
        {
            var result = await _caseService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CaseView>> GetByIdAsync(int id)
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
