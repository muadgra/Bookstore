using WebApi.DbOperations;
using System.Linq;
using System;
namespace WebApi.Application.GenreOperations.Commands.CreateGenre{
    public class CreateGenreCommand{
        public CreateGenreModel model {get; set;}
        private readonly BookStoreDbContext _context;
        public CreateGenreCommand(BookStoreDbContext context){
            _context = context;
        }

        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
            if(genre is not null){
                throw new InvalidOperationException("Kitap turu bulunmaktadir.");
            }
            genre = new Entities.Genre();
            genre.Name = model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
    public class CreateGenreModel{
        public string Name { get; set; }
    }
}