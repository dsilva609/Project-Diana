using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.Book.BookAddToShowcase;
using Project.Diana.WebApi.Features.Book.BookById;
using Project.Diana.WebApi.Features.Book.BookGetLatestAdded;
using Project.Diana.WebApi.Features.Book.BookIncrementReadCount;
using Project.Diana.WebApi.Features.Book.BookList;
using Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase;
using Project.Diana.WebApi.Features.Book.BookSubmission;
using Project.Diana.WebApi.Features.Book.BookUpdate;
using Project.Diana.WebApi.Features.Book.GetGoogleBookById;
using Project.Diana.WebApi.Features.Book.SearchGoogleBooks;
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

        [HttpPost]
        [Route("CreateBook")]
        [Authorize]
        public async Task<IActionResult> CreateBook(BookSubmission.BookSubmission submission)
            => Ok(await _mediator.Send(new BookSubmissionRequest(
                submission.Author,
                submission.Category,
                submission.CompletionStatus,
                submission.CountryOfOrigin,
                submission.CountryPurchased,
                submission.DatePurchased,
                submission.Genre,
                submission.ImageUrl,
                submission.ISBN10,
                submission.ISBN13,
                submission.IsFirstEdition,
                submission.IsHardcover,
                submission.IsNewPurchase,
                submission.IsPhysical,
                submission.IsReissue,
                submission.Language,
                submission.LinkedWishId,
                submission.LocationPurchased,
                submission.Notes,
                submission.PageCount,
                submission.Publisher,
                submission.ReadCount,
                submission.Title,
                submission.Type,
                    submission.YearReleased,
                await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookById(int id) => Ok(await _mediator.Send(new BookGetByIdRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("GetBookByVolumeId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookByVolumeId(string id) => Ok(await _mediator.Send(new GetGoogleBookByIdRequest(id)));

        [HttpGet]
        [Route("GetBookList")]
        public async Task<IActionResult> GetBookList(int itemCount, int page, string searchQuery) => Ok(await _mediator.Send(new BookListGetRequest(itemCount, page, searchQuery, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("GetLatestBooks")]
        public async Task<IActionResult> GetLatestBooks(int itemCount) => Ok(await _mediator.Send(new BookGetLatestAddedRequest(itemCount)));

        [HttpPut]
        [Route("IncrementReadCount/{id}")]
        [Authorize]
        public async Task<IActionResult> IncrementReadCount(int id) => Ok(await _mediator.Send(new BookIncrementReadCountRequest(id, await _userService.GetCurrentUser())));

        [HttpPut]
        [Route("RemoveFromShowcase/{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromShowcase(int id) => Ok(await _mediator.Send(new BookRemoveFromShowcaseRequest(id, await _userService.GetCurrentUser())));

        [HttpGet]
        [Route("SearchForBook")]
        [Authorize]
        public async Task<IActionResult> SearchForBook(string author, string title) => Ok(await _mediator.Send(new SearchGoogleBooksRequest(author, title)));

        [HttpPut]
        [Route("UpdateBook")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(BookUpdate.BookUpdate update)
            => Ok(await _mediator.Send(new BookUpdateRequest(
                update.Author,
                update.BookId,
                update.Category,
                update.CompletionStatus,
                update.CountryOfOrigin,
                update.CountryPurchased,
                update.DatePurchased,
                update.Genre,
                update.ImageUrl,
                update.ISBN10,
                update.ISBN13,
                update.IsFirstEdition,
                update.IsHardcover,
                update.IsNewPurchase,
                update.IsPhysical,
                update.IsReissue,
                update.Language,
                update.LocationPurchased,
                update.Notes,
                update.PageCount,
                update.Publisher,
                update.ReadCount,
                update.Title,
                update.Type,
                update.YearReleased,
                await _userService.GetCurrentUser()
                )));
    }
}