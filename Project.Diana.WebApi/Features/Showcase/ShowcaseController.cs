using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Showcase
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowcaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public ShowcaseController(IMediator mediator, ICurrentUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetShowcase")]
        public async Task<IActionResult> GetShowcase()
            => Ok(await _mediator.Send(new ShowcaseGetListRequest(await _userService.GetCurrentUser())));
    }
}