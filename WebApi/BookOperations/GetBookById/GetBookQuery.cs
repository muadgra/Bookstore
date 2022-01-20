using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookById{
    public class GetBookQuery{
        private readonly BookStoreDbContext _context;
        public int BookId {get; set;}
        public GetBookQuery(BookStoreDbContext context){
            _context = context;
        }

        public BookViewModel HandleBook(){
            var searchedBook = _context.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if(searchedBook is null){
                throw new InvalidOperationException("Kitap bulunamadi.");
            }
            BookViewModel book = new BookViewModel();
            book.Title = searchedBook.Title;
            book.PublishDate = searchedBook.PublishDate;
            book.PageCount = searchedBook.PageCount;
            book.GenreId = searchedBook.GenreId;
            return book;
        }

    }

    public class BookViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public int id;
    }


}