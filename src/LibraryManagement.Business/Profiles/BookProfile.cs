using AutoMapper;
using LibraryManagement.Data.Models;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Business.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDataModel, BookDomainModel>();
            CreateMap<BookDomainModel, BookDataModel>();
        }
    }
}
