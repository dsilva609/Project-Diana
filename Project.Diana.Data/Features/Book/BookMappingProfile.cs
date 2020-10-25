using AutoMapper;
using Project.Diana.Data.Features.Book.Commands;

namespace Project.Diana.Data.Features.Book
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
            => CreateMap<BookSubmissionCommand, BookRecord>()
                .ForMember(m => m.UserNum, dest => dest.MapFrom(input => input.User.UserNum));
    }
}