using System;
using System.Collections.Generic;
using System.Linq;
using TheBookStore.Contracts;
using TheBookStore.Models;

namespace TheBookStore.Repositories
{
    public class BookSampleRepository : IBookRepository
    {
        List<Book> books;

        public BookSampleRepository()
        {
            List<Author> authors = new List<Author>
            {
                new Author{Id=1,Name="Author Name 1",Surname="Author Surname 1"},
                new Author{Id=2,Name="Author Name 2",Surname="Author Surname 2"},
                new Author{Id=3,Name="Author Name 3",Surname="Author Surname 3"}
            };

            books = new List<Book> {
                new Book { Id = 1,Title="Book 1",Authors = new List<Author>{authors[0]}},
                new Book { Id = 2,Title="Book 2",Authors = new List<Author>{authors[0], authors[2]}},
                new Book { Id = 3,Title="Book 3",Authors = new List<Author>{authors[1],authors[2]}},
                new Book { Id = 4,Title="Book 4",Authors = new List<Author>{ authors[0], authors[1]}},
                new Book { Id = 5,Title="Book 5",Authors = new List<Author>{authors[2]}}
            };
        }

        public IQueryable<Book> All
        {
            get { return books.AsQueryable(); }
        }

        public Book GetOne(int id)
        {
            return books.SingleOrDefault(q => q.Id.Equals(id));
        }

        public IQueryable<Book> Search(string criteria)
        {
            return books.Where(q => q.Title.ToLower().Contains(criteria.ToLower())
                                                    || (q.Description != null && q.Description.ToLower().Contains(criteria.ToLower()))).AsQueryable();
        }
    }
}