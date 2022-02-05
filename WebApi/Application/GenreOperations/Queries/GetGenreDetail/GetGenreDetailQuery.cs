using WebApi.DbOperations;
using AutoMapper;
using System;
using System.Linq;
namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail{
    public class GetGenreDetailQuery{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreID {get; set;}
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle(){
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreID);
            if(genre is null){
                throw new InvalidOperationException("Kitap turu bulunamadi.");
            }
            GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genre);
            return returnObj;
        }
    }
    public class GenreDetailViewModel{
        public int Id {get; set;}
        public string Name {get; set;}

    }
}