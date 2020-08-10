using System;

namespace Project.Diana.Data.Features.Item
{
    public interface IBaseItemRecord
    {
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
        public string Genre { get; set; }
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public bool IsNew { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsQueued { get; set; }
        public bool IsShowcased { get; set; }
        public string Language { get; set; }
        public DateTime LastCompleted { get; set; }
        public string LocationPurchased { get; set; }
        public string Notes { get; set; }
        public int QueueRank { get; set; }
        public int TimesCompleted { get; set; }
        public string Title { get; set; }
        public string UserID { get; set; }
        public int UserNum { get; set; }
        public int YearReleased { get; set; }
    }
}