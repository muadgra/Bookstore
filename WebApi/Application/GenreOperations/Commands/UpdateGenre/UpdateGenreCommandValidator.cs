using FluentValidation;
namespace WebApi.Application.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>{
        public UpdateGenreCommandValidator(){
            RuleFor(command => command.model.Name).MinimumLength(4).When(x => x.model.Name != string.Empty);
        }
    }
}