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

namespace TheBookStore.Controllers
{
    public class AuthorsController : ApiController
    {
        IUnitOfWork unit;

        public AuthorsController(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public IHttpActionResult Get()
        {
            var authors = unit.Authors.All;
            if (!authors.Any())
            {
                return NotFound();
            }

            var response = authors.To<AuthorDto>();

            return Ok(response);

        }
    }
}
