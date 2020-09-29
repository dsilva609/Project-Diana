﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetBookList")]
        public async Task<IActionResult> GetBookList(int itemCount) => Ok(await _mediator.Send(new BookListGetRequest(itemCount, await _userService.GetCurrentUser())));
    }
}