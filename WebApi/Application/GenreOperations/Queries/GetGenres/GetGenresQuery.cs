using WebApi.DbOperations;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace WebApi.Application.GenreOperations.Queries.GetGenres{
    public class GetGenresQuery{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle(){
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }
    public class GenresViewModel{
        public int Id {get; set;}
        public string Name {get; set;}

    }
}