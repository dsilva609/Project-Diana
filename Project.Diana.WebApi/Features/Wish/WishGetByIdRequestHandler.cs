using System.Threading;
using System.Threading.Tasks;
using Kledex;
using MediatR;
using Project.Diana.Data.Features.Wish.Queries;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetByIdRequestHandler : IRequestHandler<WishGetByIDRequest, string>
    {
        private readonly IDispatcher _dispatcher;

        public WishGetByIdRequestHandler(IDispatcher dispatcher) => _dispatcher = dispatcher;

        public async Task<string> Handle(WishGetByIDRequest request, CancellationToken cancellationToken) => await _dispatcher.GetResultAsync(new WishGetByIDQuery(1, 1));
    }
}