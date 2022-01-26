using FluentValidation;
namespace WebApi.BookOperations.GetBookById{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>{
        public GetBookQueryValidator(){
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}