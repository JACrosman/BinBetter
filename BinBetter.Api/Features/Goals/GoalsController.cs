using BinBetter.Api.Data.Domain;
using BinBetter.Api.Features.Goals;
using MediatR;
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
        public Task<List<Goal>> Get()
        {
            return _sender.Send(new Get.Query());
        }
    }
}
