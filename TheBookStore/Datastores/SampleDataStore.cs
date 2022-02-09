using System;
using TheBookStore.Contracts;
using TheBookStore.Repositories;

namespace TheBookStore.Datastores
{
    public class SampleDataStore : IUnitOfWork
    {
        private IBookRepository books;
        private IAuthorRepository authors;
        private IReviewRepository reviews;

        public IBookRepository Books
        {
            get
            {
                if (books == null)
                {
                    books = new BookSampleRepository();
                }
                return books;
            }
        }

        public IAuthorRepository Authors
        {
            get
            {
                if (authors == null)
                {
                    authors = new AuthorSampleRepository();
                }

                return authors;
            }
        }

        public IReviewRepository Reviews
        {
            get
            {
                if (reviews == null)
                {
                    reviews = new ReviewSampleRepository();
                }

                return reviews;
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}