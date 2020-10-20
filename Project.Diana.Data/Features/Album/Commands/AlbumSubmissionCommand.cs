using System;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Commands
{
    public class AlbumSubmissionCommand : ICommand
    {
        public string Artist { get; }
        public string Category { get; }
        public CompletionStatusReference CompletionStatus { get; }
        public string CountryOfOrigin { get; }
        public string CountryPurchased { get; }
        public DateTime DatePurchased { get; }
        public int DiscogsId { get; }
        public string Genre { get; }
        public string ImageUrl { get; }
        public bool IsNew { get; }
        public bool IsPhysical { get; }
        public string LocationPurchased { get; }
        public MediaTypeReference MediaType { get; }
        public string Notes { get; }
        public string RecordLabel { get; }
        public SizeReference Size { get; }
        public SpeedReference Speed { get; }
        public string Style { get; }
        public int TimesCompleted { get; }
        public string Title { get; }
        public ApplicationUser User { get; }
        public int YearReleased { get; }

        public AlbumSubmissionCommand(
            string artist,
            string category,
            CompletionStatusReference completionStatus,
            string countryOfOrigin,
            string countryPurchased,
            DateTime datePurchased,
            int discogsId,
            string genre,
            string imageUrl,
            bool isNew,
            bool isPhysical,
            string locationPurchased,
            MediaTypeReference mediaType,
            string notes,
            string recordLabel,
            SizeReference size,
            SpeedReference speed,
            string style,
            int timesCompleted,
            string title,
            int yearReleased,
            ApplicationUser user
            )
        {
            Guard.Against.NullOrWhiteSpace(artist, nameof(artist));
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));

            Artist = artist;
            Category = category;
            CompletionStatus = completionStatus;
            CountryOfOrigin = countryOfOrigin;
            CountryPurchased = countryPurchased;
            DatePurchased = datePurchased;
            DiscogsId = discogsId;
            Genre = genre;
            ImageUrl = imageUrl;
            IsNew = isNew;
            IsPhysical = isPhysical;
            LocationPurchased = locationPurchased;
            MediaType = mediaType;
            Notes = notes;
            RecordLabel = recordLabel;
            Size = size;
            Speed = speed;
            Style = style;
            TimesCompleted = timesCompleted;
            Title = title;
            YearReleased = yearReleased;
            User = user;
        }
    }
}