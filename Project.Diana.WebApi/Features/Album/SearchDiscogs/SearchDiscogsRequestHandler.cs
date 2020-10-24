using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Provider.Features.Discogs;

namespace Project.Diana.WebApi.Features.Album.SearchDiscogs
{
    public class SearchDiscogsRequestHandler : IRequestHandler<SearchDiscogsRequest, IEnumerable<AlbumSearchResponse>>
    {
        private readonly IDiscogsProvider _discogsProvider;

        public SearchDiscogsRequestHandler(IDiscogsProvider discogsProvider) => _discogsProvider = discogsProvider;

        public async Task<IEnumerable<AlbumSearchResponse>> Handle(SearchDiscogsRequest request, CancellationToken cancellationToken)
        {
            var result = await _discogsProvider.SearchForAlbum(request.Album, request.Artist);

            return result.IsFailure ? throw new Exception("No results found") : result.Value;
        }
    }
}