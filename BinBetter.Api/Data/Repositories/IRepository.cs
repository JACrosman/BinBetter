namespace BinBetter.Api.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        IQueryable<T> QueryableAsync();
        Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
