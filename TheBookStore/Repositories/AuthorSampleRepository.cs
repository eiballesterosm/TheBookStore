using System.Collections.Generic;
using System.Linq;
using TheBookStore.Contracts;
using TheBookStore.Models;

namespace TheBookStore.Repositories
{
    public class AuthorSampleRepository : IAuthorRepository
    {
        List<Author> authors;

        public AuthorSampleRepository()
        {
            List<Book> bookList1 = new List<Book> { new Book { Id = 1, Title = "Book 1" }, new Book { Id = 2, Title = "Book 2" } };
            List<Book> bookList2 = new List<Book>();
            bookList2.Add(new Book { Id = 3, Title = "Book 3" });
            bookList2.Add(new Book { Id = 4, Title = "Book 4" });

            var bookList3 = new List<Book>
            {
                new Book { Id = 5, Title = "Book 5" }, new Book { Id = 6, Title = "Book 6" },new Book { Id = 7, Title = "Book 7" }, new Book { Id = 8, Title = "Book 8" }
            };

            authors = new List<Author>
            {
                new Author{Id=1,Name="Author Name 1",Surname="Author Surname 1", Books = bookList1},
                new Author{Id=2,Name="Author Name 2",Surname="Author Surname 2", Books = bookList2},
                new Author{Id=3,Name="Author Name 3",Surname="Author Surname 3", Books = bookList3}
            };
        }

        public IQueryable<Author> All
        {
            get { return authors.AsQueryable(); }
        }
    }
}