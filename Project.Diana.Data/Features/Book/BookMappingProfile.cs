using AutoMapper;
using Project.Diana.Data.Features.Book.Commands;

namespace Project.Diana.Data.Features.Book
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
            => CreateMap<BookSubmissionCommand, BookRecord>()
                .ForMember(m => m.CheckedOut, dest => dest.Ignore())
                .ForMember(m => m.DateAdded, dest => dest.Ignore())
                .ForMember(m => m.DateCompleted, dest => dest.Ignore())
                .ForMember(m => m.DateStarted, dest => dest.Ignore())
                .ForMember(m => m.DateUpdated, dest => dest.Ignore())
                .ForMember(m => m.GoogleBookID, dest => dest.Ignore())
                .ForMember(m => m.Hardcover, dest => dest.MapFrom(input => input.IsHardcover))
                .ForMember(m => m.ID, dest => dest.Ignore())
                .ForMember(m => m.IsNew, dest => dest.MapFrom(input => input.IsNewPurchase))
                .ForMember(m => m.IsQueued, dest => dest.Ignore())
                .ForMember(m => m.IsShowcased, dest => dest.Ignore())
                .ForMember(m => m.LastCompleted, dest => dest.Ignore())
                .ForMember(m => m.QueueRank, dest => dest.Ignore())
                .ForMember(m => m.UserNum, dest => dest.MapFrom(input => input.User.UserNum));
    }
}