using System;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.Data.Features.Album
{
    public class AlbumRecord : IBaseItemRecord
    {
        public string Artist { get; set; }
        public string Category { get; set; }
        public bool CheckedOut { get; set; }
        public CompletionStatusReference CompletionStatus { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CountryPurchased { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DatePurchased { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int DiscogsId { get; set; }
        public string Genre { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsNew { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsQueued { get; set; }
        public bool IsShowcased { get; set; }
        public string Language { get; set; }
        public DateTime LastCompleted { get; set; }
        public string LocationPurchased { get; set; }
        public MediaTypeReference MediaType { get; set; }
        public string Notes { get; set; }
        public int QueueRank { get; set; }
        public string RecordLabel { get; set; }
        public SizeReference Size { get; set; }
        public SpeedReference Speed { get; set; }
        public string Style { get; set; }
        public int TimesCompleted { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public int UserNum { get; set; }
        public int YearReleased { get; set; }
    }
}