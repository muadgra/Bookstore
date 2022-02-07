using WebApi.Entities;
using System;
using WebApi.DbOperations;
namespace TestSetup{
    public static class Genres{
        public static void AddGenres(this BookStoreDbContext context){
            context.Genres.AddRange(
                    new Genre{
                        Name = "PersonalGrowth",          
                    },
                    new Genre{
                        Name = "Science Fiction",   
                    },
                    new Genre{
                        Name = "Romance",                    
                    }
                );
        }
    }

}