using System;
using WebApi.Entities;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace WebApi.DbOperations{

    public class DataGenerator{
        public static void Initiliaze(IServiceProvider serviceProvider){
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()) return;
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
                context.Books.AddRange(
            new Book{
                Title = "Lean Startup",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001, 6, 12)
            },
            new Book{
                Title = "Herland",
                GenreId = 2,
                PageCount = 250,
                PublishDate = new DateTime(2010, 5, 23)
            },
            new Book{
                Title = "Dune",
                GenreId = 2,
                PageCount = 540,
                PublishDate = new DateTime(2001, 1, 12)
            });
            context.SaveChanges();
            }
        }

    }


}