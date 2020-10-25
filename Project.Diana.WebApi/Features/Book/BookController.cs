using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Book.BookAddToShowcase;
using Project.Diana.WebApi.Features.Book.BookById;
using Project.Diana.WebApi.Features.Book.BookList;
using Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Features.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public BookController(IMediator mediator, ICurrentUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPut]
        [Route("AddToShowcase/{id}")]
        [Authorize]
        public async Task<IActionResult> AddToShowcase(int id) => Ok(await _mediator.Send(new BookAddToShowcaseRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookById(int id) => Ok(await _mediator.Send(new BookGetByIdRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("GetBookList")]
        public async Task<IActionResult> GetBookList(int itemCount, int page, string searchQuery) => Ok(await _mediator.Send(new BookListGetRequest(itemCount, page, searchQuery, await _userService.GetCurrentUser())));

        [HttpPut]
        [Route("RemoveFromShowcase/{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromShowcase(int id) => Ok(await _mediator.Send(new BookRemoveFromShowcaseRequest(id, await _userService.GetCurrentUser())));
    }
}