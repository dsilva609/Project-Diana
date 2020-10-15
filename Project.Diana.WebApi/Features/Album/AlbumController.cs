using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Album.AlbumAddToShowcase;
using Project.Diana.WebApi.Features.Album.AlbumById;
using Project.Diana.WebApi.Features.Album.AlbumClearShowcase;
using Project.Diana.WebApi.Features.Album.AlbumList;
using Project.Diana.WebApi.Features.Album.AlbumRemoveFromShowcase;
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

        [HttpPut]
        [Route("AddToShowcase/{id}")]
        [Authorize]
        public async Task<IActionResult> AddToShowcase(int id) => Ok(await _mediator.Send(new AlbumAddToShowcaseRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("ClearShowcase")]
        [Authorize]
        public async Task<IActionResult> ClearShowcase() => Ok(await _mediator.Send(new AlbumClearShowcaseRequest(await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAlbumById(int id) => Ok(await _mediator.Send(new AlbumGetByIdRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("GetAlbumList")]
        public async Task<IActionResult> GetAlbumList(int itemCount, int page, string searchQuery) => Ok(await _mediator.Send(new AlbumListGetRequest(itemCount, page, searchQuery, await _userService.GetCurrentUser())));

        [HttpPut]
        [Route("RemoveFromShowcase/{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromShowcase(int id) => Ok(await _mediator.Send(new AlbumRemoveFromShowcaseRequest(id, await _userService.GetCurrentUser())));
    }
}