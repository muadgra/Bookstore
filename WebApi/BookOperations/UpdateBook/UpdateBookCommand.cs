using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook{
    public class UpdateBookCommand{
        private readonly BookStoreDbContext _context;
        public UpdatedBook Model {get; set;}
        public int BookId {get; set;}
        public UpdateBookCommand(BookStoreDbContext context){
            _context = context;
        }

        public void Update(){
            var book = _context.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("Boyle bir kitap bulunamadi.");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default || Model.Title != "string" ? Model.Title : book.Title;
            _context.SaveChanges();

        }




        
    }
    public class UpdatedBook{
        public string Title {get; set;}
        public int GenreId {get; set;}
        public int PageCount {get; set;}
        public DateTime PublishDate {get; set;}    
    }
}