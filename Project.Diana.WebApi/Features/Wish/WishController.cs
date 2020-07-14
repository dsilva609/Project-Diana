using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Wish
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public WishController(ICurrentUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetWish")]
        [Authorize]
        public async Task<IActionResult> GetWish(int id)
        {
            var user = await _userService.GetCurrentUser();

            return Ok(await _mediator.Send(new WishGetByIDRequest(id)));
        }
    }
}