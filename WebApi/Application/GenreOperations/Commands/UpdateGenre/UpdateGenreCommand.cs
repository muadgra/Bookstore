using WebApi.DbOperations;
using System.Linq;
using System;
namespace WebApi.Application.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommand{
        public int GenreId {get; set;}
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context){
            _context = context;
        }
        public UpdateGenreModel model {get; set;}
        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null){
                throw new InvalidOperationException("Boyle bir kitap turu bulunamadi.");
            }
            if(_context.Genres.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != GenreId)){
                throw new InvalidOperationException("Bu isimde bir kitap turu mevcut.");
            }
            genre.Name = model.Name.Trim() == default ? genre.Name : model.Name;
            genre.IsActive = model.IsActive;
            _context.SaveChanges();
        }

    }
    public class UpdateGenreModel{
        public string Name {get; set;}
        public bool IsActive {get; set;} = true;
    }
}