using Xunit;
using TestSetup;
using FluentAssertions;
using WebApi.DbOperations;
using System;
using WebApi.Application.BookOperations.Commands.CreateBook;
using AutoMapper;
using WebApi.Entities;
namespace Application.BookOperations.Commands.CreateCommand{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrange
            var book = new Book(){Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990, 01, 11), GenreId = 2};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookCommand.CreateBookModel(){Title = book.Title};
            //act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut.");
            //assert
        }
    }
}