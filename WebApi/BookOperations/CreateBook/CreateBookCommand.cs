using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;
using AutoMapper;
namespace WebApi.BookOperations.CreateBook{
    public class CreateBookCommand{
        public CreateBookModel Model {get; set;}
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            
            if(book is not null){
                throw new InvalidOperationException("Kitap mevcut.");
            }
            book = _mapper.Map<Book>(Model);
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