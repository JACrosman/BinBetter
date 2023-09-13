using BinBetter.Api.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BinBetter.Api.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public class UsersController : ControllerBase
    {
        private ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] Authenticate.Command command)
        {
            var user = await _sender.Send(command);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<UserModelEnvelope> Create([FromBody] Create.Command command)
        {
            return await _sender.Send(command);
        }
    }
}
