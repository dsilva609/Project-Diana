using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Project.Diana.WebApi.Features.Wish
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetWish(int id) => Ok(await _mediator.Send(new WishGetByIDRequest(id)));
    }
}