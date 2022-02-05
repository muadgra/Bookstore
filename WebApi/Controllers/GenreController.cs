using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
namespace WebApi.Controllers{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetGenres(){
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpGet("id")]
        public ActionResult GetGenreDetail(int id){
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreID = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre){
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre){
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.model = updatedGenre;
            command.Handle();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteGenre(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}