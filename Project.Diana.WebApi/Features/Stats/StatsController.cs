using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Stats
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public StatsController(ICurrentUserService currentUserService, IMediator mediator)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetGlobalStats")]
        public async Task<IActionResult> GetGlobalStats() => Ok(await _mediator.Send(new GlobalStatsGetRequest()));

        [HttpGet]
        [Route("GetUserStats")]
        [Authorize]
        public async Task<IActionResult> GetUserStats() => Ok(await _mediator.Send(new UserStatsGetRequest((await _currentUserService.GetCurrentUser()).UserNum)));
    }
}