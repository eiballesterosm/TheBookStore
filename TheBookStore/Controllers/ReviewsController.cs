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
using TheBookStore.Models;

namespace TheBookStore.Controllers
{
    public class ReviewsController : ApiController
    {
        IUnitOfWork unit;

        public ReviewsController()
        {
            unit = new SampleDataStore();
        }

        public IHttpActionResult Get(int bookId)
        {
            var reviews = unit.Reviews.All(bookId);

            if (!reviews.Any())
            {
                return NotFound();
            }

            var response = reviews.To<ReviewDto>();
            return Ok(response);
        }

        public IHttpActionResult Post([FromBody] Review review, int bookId)
        {
            review.BookId = bookId;
            var newReview = unit.Reviews.AddReview(review);
            unit.Commit();

            var url = Url.Link("DefaultApi", new { controller = "Reviews", id = newReview.Id });

            return Created(url, newReview);
        }

        public IHttpActionResult Delete(int id)
        {
            unit.Reviews.RemoveReview(id);
            unit.Commit();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
