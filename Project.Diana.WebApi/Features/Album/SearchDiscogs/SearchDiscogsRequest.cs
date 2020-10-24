using System;
using System.Collections.Generic;
using MediatR;
using Project.Diana.ApiClient.Features.Discogs;

namespace Project.Diana.WebApi.Features.Album.SearchDiscogs
{
    public class SearchDiscogsRequest : IRequest<IEnumerable<SearchResult>>
    {
        public string Album { get; }
        public string Artist { get; }

        public SearchDiscogsRequest(string album, string artist)
        {
            if (string.IsNullOrWhiteSpace(album) && string.IsNullOrWhiteSpace(artist))
            {
                throw new ArgumentException("Album and artist are missing", "album, Artist");
            }

            Album = album;
            Artist = artist;
        }
    }
}