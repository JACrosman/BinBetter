namespace BinBetter.Api.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        IQueryable<T> AsQueryable();
        Task<T> FindByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
