using Xunit;
using TestSetup;
using FluentAssertions;
using WebApi.DbOperations;
using System;
using WebApi.Application.BookOperations.Commands.CreateBook;
using AutoMapper;
using WebApi.Entities;
namespace Application.BookOperations.Commands.CreateCommand{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>{

        [Theory]
        [InlineData("Lord of the Rings", 0, 0)]
        [InlineData("Lord of the Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lord of the Rings", 100, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors(string title, int pageCount, int genreId){
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel(){
                Title = title, PageCount = pageCount, PublishDate = DateTime.Now.Date.AddYears(-1), GenreId = genreId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel(){
                Title = "Lord of the Rings", PageCount = 100, PublishDate = DateTime.Now.Date, GenreId = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var ressult = validator.Validate(command);
            ressult.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}