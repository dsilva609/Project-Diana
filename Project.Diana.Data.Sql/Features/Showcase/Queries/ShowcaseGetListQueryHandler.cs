﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Showcase.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Showcase.Queries
{
    public class ShowcaseGetListQueryHandler : IQueryHandler<ShowcaseGetListQuery, ShowcaseListResponse>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public ShowcaseGetListQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<ShowcaseListResponse> Handle(ShowcaseGetListQuery query)
        {
            var response = new ShowcaseListResponse();

            var showcasedAlbums = await _context.Albums.Where(album => album.IsShowcased && album.UserNum == query.UserId).ToListAsync();
            response.ShowcasedAlbums = showcasedAlbums;

            var showcasedBooks = await _context.Books.Where(book => book.IsShowcased && book.UserNum == query.UserId).ToListAsync();
            response.ShowcasedBooks = showcasedBooks;

            return response;
        }
    }
}