using AutoMapper;
using Project.Diana.Data.Features.Album.Commands;

namespace Project.Diana.Data.Features.Album
{
    public class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
            => CreateMap<AlbumSubmissionCommand, AlbumRecord>()
                .ForMember(m => m.CheckedOut, dest => dest.Ignore())
                .ForMember(m => m.DateAdded, dest => dest.Ignore())
                .ForMember(m => m.DateCompleted, dest => dest.Ignore())
                .ForMember(m => m.DateStarted, dest => dest.Ignore())
                .ForMember(m => m.DateUpdated, dest => dest.Ignore())
                .ForMember(m => m.ID, dest => dest.Ignore())
                .ForMember(m => m.IsQueued, dest => dest.Ignore())
                .ForMember(m => m.IsShowcased, dest => dest.Ignore())
                .ForMember(m => m.Language, dest => dest.Ignore())
                .ForMember(m => m.LastCompleted, dest => dest.Ignore())
                .ForMember(m => m.QueueRank, dest => dest.Ignore())
                .ForMember(m => m.UserNum, dest => dest.MapFrom(input => input.User.UserNum));
    }
}