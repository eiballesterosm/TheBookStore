using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheBookStore.App_Start;
using TheBookStore.Contracts;
using TheBookStore.Datastores;
using TheBookStore.DTO;
using TheBookStore.Infrastructure;

namespace TheBookStore.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IUnitOfWork unit;

        public BooksController()
        {
            this.unit = new SampleDataStore();
        }

        public IHttpActionResult Get()
        {
            var books = unit.Books.All;

            if (!books.Any())
            {
                return NotFound();
            }

            var response = books.To<BookDto>();

            return Ok(response);
        }


        public IHttpActionResult Get(string query)
        {
            var books = unit.Books.Search(query);

            if (!books.Any())
            {
                return NotFound();
            }

            var response = books.To<BookDto>();
            return Ok(response);
        }

        [CheckNullsAttribute]
        public IHttpActionResult Get(int id)
        {
            var book = unit.Books.GetOne(id);
            if (book == null)
            {
                return NotFound();
            }

            var response = book.To<BookDto>();
            return Ok(response);
        }
    }
}
