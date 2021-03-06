using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Wish.CompleteItem;
using Project.Diana.WebApi.Features.Wish.Delete;
using Project.Diana.WebApi.Features.Wish.Retrieve;
using Project.Diana.WebApi.Features.Wish.Submission;
using Project.Diana.WebApi.Features.Wish.Update;
using Project.Diana.WebApi.Features.Wish.WishList;
using Project.Diana.WebApi.Helpers.User;

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
        [Route("CompleteWish")]
        [Authorize]
        public async Task<IActionResult> CompleteWish(int id) => Ok(await _mediator.Send(new WishCompleteItemRequest(await _userService.GetCurrentUser(), id)));

        [HttpPost]
        [Route("CreateWish")]
        [Authorize]
        public async Task<IActionResult> CreateWish(WishSubmission wishSubmission)
            => Ok(await _mediator.Send(new WishSubmissionRequest(
                wishSubmission.ApiId,
                wishSubmission.Category,
                wishSubmission.ImageUrl,
                wishSubmission.ItemType,
                wishSubmission.Notes,
                wishSubmission.Owned,
                wishSubmission.Title,
                await _userService.GetCurrentUser())));

        [HttpDelete]
        [Route("DeleteWish")]
        [Authorize]
        public async Task<IActionResult> DeleteWish(int id) => Ok(await _mediator.Send(new WishDeleteRequest(await _userService.GetCurrentUser(), id)));

        [HttpGet]
        [Route("GetWish")]
        [Authorize]
        public async Task<IActionResult> GetWish(int id) => Ok(await _mediator.Send(new WishGetByIdRequest(await _userService.GetCurrentUser(), id)));

        [HttpGet]
        [Route("GetWishList")]
        [Authorize]
        public async Task<IActionResult> GetWishList() => Ok(await _mediator.Send(new WishGetListByUserIdRequest(await _userService.GetCurrentUser())));

        [HttpPost]
        [Route("UpdateWish")]
        [Authorize]
        public async Task<IActionResult> UpdateWish(WishUpdate update)
            => Ok(await _mediator.Send(new WishUpdateRequest(
                update.ApiId,
                update.Category,
                update.ImageUrl,
                update.ItemType,
                update.Notes,
                update.Owned,
                update.Title,
                await _userService.GetCurrentUser(),
                update.WishId)));
    }
}