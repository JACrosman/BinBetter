using BinBetter.Api.Data.Domain;
using BinBetter.Api.Features.Goals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BinBetter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly ISender _sender;

        public GoalsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Authorize]
        public Task<GoalListEnvelope> Get()
        {
            return _sender.Send(new Get.Query());
        }

        [HttpGet("{id}")]
        [Authorize]
        public Task<GoalEnvelope> Get(int id)
        {
            return _sender.Send(new GetById.Query(id));
        }

        [HttpPost]
        [Authorize]
        public Task<GoalEnvelope> Create([FromBody] Create.Command command)
        {
            return _sender.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize]
        public Task<GoalEnvelope> Update(int id, [FromBody] Update.Model model)
        {
            return _sender.Send(new Update.Command(id, model));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public Task Delete(int id)
        {
            return _sender.Send(new Delete.Command(id));
        }
    }
}
