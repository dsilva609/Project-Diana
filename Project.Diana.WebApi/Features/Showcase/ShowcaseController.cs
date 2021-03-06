using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;

namespace Project.Diana.WebApi.Features.Showcase
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowcaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowcaseController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("GetShowcase")]
        public async Task<IActionResult> GetShowcase(int userId)
            => Ok(await _mediator.Send(new ShowcaseGetListRequest(userId)));
    }
}