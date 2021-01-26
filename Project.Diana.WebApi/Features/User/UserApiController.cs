using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.WebApi.Features.User.Login;

namespace Project.Diana.WebApi.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserApiController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) => await _mediator.Send(request);
    }
}