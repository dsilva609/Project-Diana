using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Project.Diana.WebApi.Features.Stats
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatsApiController(IMediator mediator) => _mediator = mediator;
    }
}