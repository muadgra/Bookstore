using System;
using WebApi.DbOperations;
using System.Linq;

namespace WebApi.Application.BookOperations.Commands.DeleteBook{
    public class DeleteBookCommand{
        private readonly BookStoreDbContext _context;
        public int BookId {get; set;}
        public DeleteBookCommand(BookStoreDbContext context){
            _context = context;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null){
                throw new InvalidOperationException("Kitap bulunmuyor.");
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}