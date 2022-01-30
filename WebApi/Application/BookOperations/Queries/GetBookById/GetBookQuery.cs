using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;
namespace WebApi.Application.BookOperations.Queries.GetBookById{
    public class GetBookQuery{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int BookId {get; set;}
        public GetBookQuery(BookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public BookViewModel HandleBook(){
            var searchedBook = _context.Books.Include(x=> x.Genre).Where(book => book.Id == BookId).SingleOrDefault();
            if(searchedBook is null){
                throw new InvalidOperationException("Kitap bulunamadi.");
            }
            BookViewModel book = _mapper.Map<BookViewModel>(searchedBook);
            return book;
        }

    }

    public class BookViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public GenreEnum Genre { get; set; }
        public int id;
    }


}