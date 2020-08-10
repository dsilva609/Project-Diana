using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Album
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public AlbumController(IMediator mediator, ICurrentUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAlbumList")]
        public async Task<IActionResult> GetAlbumList() => Ok(await _mediator.Send(new AlbumListGetRequest(await _userService.GetCurrentUser())));
    }
}