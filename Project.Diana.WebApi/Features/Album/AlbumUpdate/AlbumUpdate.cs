using System;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.WebApi.Features.Album.AlbumUpdate
{
    public class AlbumUpdate
    {
        public int AlbumId { get; set; }
        public string Artist { get; set; }
        public string Category { get; set; }
        public CompletionStatusReference CompletionStatus { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CountryPurchased { get; set; }
        public DateTime DatePurchased { get; set; }
        public int DiscogsId { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public bool IsNewPurchase { get; set; }
        public bool IsPhysical { get; set; }
        public string LocationPurchased { get; set; }
        public MediaTypeReference MediaType { get; set; }
        public string Notes { get; set; }
        public int PlayCount { get; set; }
        public string RecordLabel { get; set; }
        public SizeReference Size { get; set; }
        public SpeedReference Speed { get; set; }
        public string Style { get; set; }
        public string Title { get; set; }
        public int YearReleased { get; set; }
    }
}