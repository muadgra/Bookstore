using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
namespace WebApi.Common{
    public class MappingProfile : Profile{
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
        }
    }

}