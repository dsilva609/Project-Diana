using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Wish
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public WishController(IMediator mediator, ICurrentUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetWish")]
        [Authorize]
        public async Task<IActionResult> GetWish(int id) => Ok(await _mediator.Send(new WishGetByIDRequest(await _userService.GetCurrentUser(), id)));

        [HttpGet]
        [Route("GetWishList")]
        [Authorize]
        public async Task<IActionResult> GetWishList() =>
            Ok(await _mediator.Send(new WishGetListByUserIDRequest(await _userService.GetCurrentUser())));
    }
}