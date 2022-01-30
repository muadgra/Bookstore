using FluentValidation;
namespace WebApi.Application.BookOperations.Queries.GetBookById{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>{
        public GetBookQueryValidator(){
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}