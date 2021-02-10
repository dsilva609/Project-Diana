using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.User.RefreshToken
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public RefreshTokenRequestHandler(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}