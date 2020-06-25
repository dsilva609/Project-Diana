using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetByIdRequestHandler : IRequestHandler<WishGetByIDRequest, string>
    {
        public async Task<string> Handle(WishGetByIDRequest request, CancellationToken cancellationToken)
            => await Task.FromResult("test");
    }
}