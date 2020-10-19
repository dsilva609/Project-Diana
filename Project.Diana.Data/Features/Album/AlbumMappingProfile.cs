using AutoMapper;
using Project.Diana.Data.Features.Album.Commands;

namespace Project.Diana.Data.Features.Album
{
    public class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
            => CreateMap<AlbumSubmissionCommand, AlbumRecord>()
                .ForMember(m => m.UserNum, dest => dest.MapFrom(input => input.User.UserNum));
    }
}