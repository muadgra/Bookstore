using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook{
    public class CreateBookCommand{
        public CreateBookModel Model {get; set;}
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context){
            _context = context;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            
            if(book is not null){
                throw new InvalidOperationException("Kitap mevcut.");
            }
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            _context.Books.Add(book);
            _context.SaveChanges();
            
        }
        public class CreateBookModel{
            public string Title {get; set;}
            public int GenreId {get; set;}
            public int PageCount {get; set;}
            public DateTime PublishDate {get; set;}
        }


    }
    
}