using System;
using System.Threading.Tasks;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Commands
{
    public class WishCompleteItemCommandHandler : ICommandHandler<WishCompleteItemCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public WishCompleteItemCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public Task Handle(WishCompleteItemCommand command) => throw new NotImplementedException();
    }
}