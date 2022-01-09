﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //UI'a dönecek veri setini ViewModel ile koruyarak döndüreceğiz.
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach(var item in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = item.Title,
                    PageCount = item.PageCount,
                    PublishDate = item.PublishDate.Date.ToString("dd/MM/yyy"),
                    Genre = ((GenreEnum) item.GenreId).ToString(),
                });
            }
            return vm;
        }
   
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}