using System.Linq;
using TheBookStore.Models;

namespace TheBookStore.Contracts
{
    public interface IReviewRepository
    {
        IQueryable<Review> All(int bookId);

        Review AddReview(Review review);

        Review RemoveReview(int id);
    }
}
