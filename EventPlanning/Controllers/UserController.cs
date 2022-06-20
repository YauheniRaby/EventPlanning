using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IdentityServer4.Extensions;
using System.Threading.Tasks;
using System.Linq;

namespace EventPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptionsMonitor<AppMessages> _appMessages;
        private readonly IOptionsMonitor<MessageTemplates> _messageTemplates;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(
            IUserService userService, 
            IOptionsMonitor<AppMessages> appMessages, 
            IOptionsMonitor<MessageTemplates> messageTemplates,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _appMessages = appMessages;
            _messageTemplates = messageTemplates;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> RegistrationAsync ([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Count() != 0)
            {
                return Forbid();
            }
            if (await _userService.ExistsAsync(userCredentialsDto.Login))
            {
                return BadRequest(_appMessages.CurrentValue.UserExist);
            }
            await _userService.Registration(userCredentialsDto);
            return Ok();
        }

        [HttpGet ("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailDTO confirmEmailDto)
        {
            if (await _userService.ExistsAsync(confirmEmailDto.UserId))
            {
                if(await _userService.ConfirmEmailAsync(confirmEmailDto))
                    return Ok(_messageTemplates.CurrentValue.AcceptedCode);
            }
            return BadRequest();
        }

        [HttpPost("MoreInformation")]
        [Authorize]
        public async Task<ActionResult> AddMoreInformation([FromBody] UserInformationDTO userInformationDto)
        {
            var userId = int.TryParse(_httpContextAccessor.HttpContext.User.GetSubjectId(), out var value)? value: default;
            if (await _userService.ExistsAsync(userId))
            {
                await _userService.AddMoreInformation(userInformationDto, userId);
                return Ok();
            }
            return BadRequest();
        }
    }
}
