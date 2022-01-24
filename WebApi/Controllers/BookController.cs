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
            GetBookQuery query = new GetBookQuery(_context, _mapper);
            query.BookId = id;
            var book = query.HandleBook();
            
            return Ok(book);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
            try{
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBook updatedBook){
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try{
                command.BookId = id;
                command.Model = updatedBook;
                command.Update();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            try{
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
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