using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBookById;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;
using FluentValidation;

namespace WebApi.AddControllers{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult getBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        //Get id from route.
        [HttpGet("{id}")]
        public IActionResult getByBookId(int id)
        {
            BookViewModel book;
            GetBookQuery query = new GetBookQuery(_context, _mapper);
            query.BookId = id;
            GetBookQueryValidator validator = new GetBookQueryValidator();
            validator.ValidateAndThrow(query);
            book = query.HandleBook();
            
            
            return Ok(book);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
                
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBook updatedBook){
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Update();
            
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
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