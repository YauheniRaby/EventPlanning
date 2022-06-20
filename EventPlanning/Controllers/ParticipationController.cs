using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace EventPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IParticipationService _participationService;
        private readonly IOptionsMonitor<AppMessages> _optionsMonitor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParticipationController(
            ICaseService caseService, 
            IOptionsMonitor<AppMessages> optionsMonitor, 
            IParticipationService participationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _caseService = caseService;
            _participationService = participationService;
            _optionsMonitor = optionsMonitor;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("sms")]
        [Authorize]
        public async Task<ActionResult> AddParticipationAsync([FromBody] ParticipationDTO participationDTO)
        {
            if (await _caseService.CanAddedAsync(participationDTO.CaseId))
            {
                var userId = int.TryParse(_httpContextAccessor.HttpContext.User.GetSubjectId(), out var value) ? value : default; 
                var result =  await _participationService.AddParticipationAsync(participationDTO, userId);
                return Ok(result);
            }
            return BadRequest(_optionsMonitor.CurrentValue.CaseNotExist);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> VerifiAsync([FromBody] ParticipationVerifiDTO participationDTO)
        {
            var userId = int.TryParse(_httpContextAccessor.HttpContext.User.GetSubjectId(), out var value) ? value : default;
            if (await _participationService.ExistsAsync(participationDTO.Id, userId))
            {
                if(await _participationService.VerifiAsync(participationDTO))
                    return Ok();
                return NotFound();
            }
            return BadRequest(_optionsMonitor.CurrentValue.CaseNotExist);
        }
    }
}
