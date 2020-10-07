using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Book.BookById;
using Project.Diana.WebApi.Features.Book.BookList;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookById(int id) => Ok(await _mediator.Send(new BookGetByIdRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("GetBookList")]
        public async Task<IActionResult> GetBookList(int itemCount, int page) => Ok(await _mediator.Send(new BookListGetRequest(itemCount, page, await _userService.GetCurrentUser())));
    }
}