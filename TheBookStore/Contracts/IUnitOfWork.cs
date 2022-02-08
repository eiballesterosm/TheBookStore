namespace TheBookStore.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IReviewRepository Reviews { get; }
    }
}
