using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context){
            _context = context;
        }
        [HttpGet]
        public IActionResult getBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        //Get id from route.
        [HttpGet("{id}")]
        public IActionResult getByBookId(int id)
        {
            GetBookQuery query = new GetBookQuery(_context);
            var book = query.HandleBook(id);
     
            return Ok(book);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context);
            
            try{
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book == null){
                return BadRequest();
            }
            //if there is a new genre id, change it
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default || updatedBook.Title != "string" ? updatedBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id){
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book == null){
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}
    }

}