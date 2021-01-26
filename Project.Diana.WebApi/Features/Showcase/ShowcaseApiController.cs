using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;

namespace Project.Diana.WebApi.Features.Showcase
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowcaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowcaseApiController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("GetShowcase")]
        public async Task<IActionResult> GetShowcase(int userID)
            => Ok(await _mediator.Send(new ShowcaseGetListRequest(userID)));
    }
}